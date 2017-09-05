using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;

namespace SmartCopier
{
	public class SmartCopier : EditorWindow
	{
		private enum CopyMode
		{
			ReplaceValues,
			PasteAsNew
		}

		private const int Order = 10000000;
		public static readonly BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public;

		public static GUIStyle MainLabelStyle { get; private set; }
		public static GUIStyle ListLabelStyle { get; private set; }

		private static GameObject m_GameObjectToCopyFrom;
		private static CopiedComponent[] m_CopiedComponents;
		private static Vector2 m_ScrollPosition;

		private static CopyMode m_CopyMode;

		[MenuItem("CONTEXT/Component/Smart Copy Components", false, Order)]
		private static void ShowWindow(MenuCommand menuCommand)
		{
			m_GameObjectToCopyFrom = GetTargetObject(menuCommand);
			BuildWindow();
		}

		[MenuItem("CONTEXT/Component/Smart Paste Components", false, Order + 1)]
		private static void PasteComponents(MenuCommand menuCommand)
		{
			GameObject go = GetTargetObject(menuCommand);
			PasteComponents(go);
		}

		[MenuItem("CONTEXT/Component/Smart Paste Components", true, Order + 1)]
		private static bool ValidatePasteComponents(MenuCommand menuCommand)
		{
			return m_CopiedComponents != null && m_CopiedComponents.Length > 0 &&
				GetTargetObject(menuCommand) != m_GameObjectToCopyFrom;
		}

		private void OnSelectionChange()
		{
			Repaint();
		}

		private static void BuildWindow()
		{
			Component[] components = m_GameObjectToCopyFrom.GetComponents<Component>();
			CreateCopiedComponents(components);
			GetWindow<SmartCopier>(false, "SmartCopier");
		}

		private static void PasteComponents(GameObject gameObject)
		{
			Undo.RecordObject(gameObject, "Copying components");
			foreach (CopiedComponent copied in m_CopiedComponents.Where(c => c.Checked))
			{
				Type componentType = copied.Component.GetType();
				if (m_CopyMode == CopyMode.PasteAsNew)
				{
					Component newComponent = Undo.AddComponent(gameObject, componentType);
					if (newComponent != null)
					{
						EditorUtility.CopySerialized(copied.Component, newComponent);
						Copy(copied, newComponent);
					}
				}
				else
				{
					Component otherComponent = gameObject.GetComponent(componentType);
					if (otherComponent == null)
					{
						otherComponent = Undo.AddComponent(gameObject, componentType);
					}
					Copy(copied, otherComponent);
				}
				
				EditorUtility.SetDirty(gameObject);
			}
		}

		private static void Copy(CopiedComponent source, Component target)
		{
			// Try default Component copying first.
			if (CanCopySerialized(source))
			{
				EditorUtility.CopySerialized(source.Component, target);
			}
			else // use custom copying logic.
			{
				source.CopyTo(target);
			}
		}

		// If all checkboxes are checked, we can use Unity copying system.
		private static bool CanCopySerialized(CopiedComponent copied)
		{
			return copied.PropertyCheckboxes.All(prop => prop.CanCopy()) &&
			       copied.FieldCheckboxes.All(field => field.CanCopy());
		}

		private bool HasCheckedAny()
		{
			for (int i = 0; i < m_CopiedComponents.Length; ++i)
			{
				CopiedComponent copied = m_CopiedComponents[i];
				if (copied.Checked &&
					(copied.PropertyCheckboxes.Any(prop => prop.CanCopy()) ||
				    copied.FieldCheckboxes.Any(field => field.CanCopy())))
				{
					return true;
				}
			}
			return false;
		}

		private static GameObject GetTargetObject(MenuCommand menuCommand)
		{
			Component component = (Component)menuCommand.context;
			return component.gameObject;
		}

		private static void CreateCopiedComponents(Component[] components)
		{
			m_CopiedComponents = new CopiedComponent[components.Length];
			for (int i = 0; i < components.Length; ++i)
			{
				m_CopiedComponents[i] = new CopiedComponent(components[i]);
			}
		}

		private static void CreateStyle()
		{
			MainLabelStyle = new GUIStyle(GUI.skin.label);
			MainLabelStyle.fontStyle = FontStyle.Bold;
			MainLabelStyle.padding = new RectOffset(5, MainLabelStyle.padding.right, MainLabelStyle.padding.top, MainLabelStyle.padding.bottom);

			ListLabelStyle = new GUIStyle(GUI.skin.label);
			ListLabelStyle.padding = MainLabelStyle.padding;

			GUI.skin.label.wordWrap = true;
		}

		protected void OnGUI()
		{
			// Close the window if our data becomes invalid.
			if (m_CopiedComponents == null)
			{
				GetWindow<SmartCopier>().Close();
				return;
			}
			// Create the label styles once.
			if (MainLabelStyle == null) CreateStyle();

			m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label(string.Format("Select {0}'s Components and their properties to copy.", m_GameObjectToCopyFrom.name));
			if (GUILayout.Button(new GUIContent("Refresh", "Refresh all components and properties in case components were added or removed.")))
			{
				BuildWindow();
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			DrawCopyMode();
			EditorGUILayout.Space();

			for (int i = 0; i < m_CopiedComponents.Length; ++i)
			{
				m_CopiedComponents[i].Draw();
				EditorGUILayout.Space();
			}

			if (HasValidSelection())
			{
				if (HasCheckedAny())
				{
					GUILayout.Label("Press the button to copy the Components to the selected GameObjects.");
					if (GUILayout.Button(new GUIContent("Smart Paste Components", "Paste all checked components and properties into the selected GameObjects."),
						GUILayout.Height(30f)))
					{
						foreach (GameObject go in Selection.gameObjects)
						{
							PasteComponents(go);
						}
					}
				}
				else
				{
					GUILayout.Label("Check any Components or properties to copy them to the selected GameObjects.");
				}
				
				DrawSelectedGameObjects();
			}
			else
			{
				GUILayout.Label("Select GameObjects to copy the Components to.");
			}

			EditorGUILayout.Space();
			EditorGUILayout.EndScrollView();
		}

		private static void DrawCopyMode()
		{
			GUILayout.BeginHorizontal();
			m_CopyMode = (CopyMode)EditorGUILayout.EnumPopup(m_CopyMode, GUILayout.MaxWidth(100));
			string label = m_CopyMode == CopyMode.PasteAsNew
				? "Create new components with the selected properties."
				: "Replace existing values, or create new.";

			GUILayout.Label(label);
			GUILayout.EndHorizontal();
		}

		private static void DrawSelectedGameObjects()
		{
			GUILayout.Label("Selected GameObjects:");
			GUI.enabled = false;
			GUI.color = new Color(1f, 1f, 1f, 1.25f);
			foreach (GameObject go in Selection.gameObjects)
			{
				EditorGUILayout.ObjectField(go, go.GetType(), true);
			}
			GUI.color = Color.white;
			GUI.enabled = true;
		}

		private static bool HasValidSelection()
		{
			GameObject[] selectedGameObjects = Selection.gameObjects;
			return selectedGameObjects.Length > 0 &&
			       (selectedGameObjects.Length != 1 || selectedGameObjects[0] != m_GameObjectToCopyFrom);
		}
	}
}
