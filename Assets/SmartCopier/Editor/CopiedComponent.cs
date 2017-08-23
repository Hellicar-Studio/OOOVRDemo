using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SmartCopier
{
	public class CopiedComponent
	{
		public Component Component { get; private set; }
		public MemberCheckbox<PropertyInfo>[] PropertyCheckboxes { get; private set; }
		public MemberCheckbox<FieldInfo>[] FieldCheckboxes { get; private set; }
		public bool Checked { get; private set; }

		private bool m_Foldout;

		public CopiedComponent(Component component)
		{
			Component = component;
			Checked = true;
			Type tye = Component.GetType();
			List<PropertyInfo> properties = tye.GetProperties(SmartCopier.Flags).ToList();
			List<FieldInfo> fields = tye.GetFields(SmartCopier.Flags | BindingFlags.NonPublic).ToList();

			FilterMembers(properties, fields);

			PropertyCheckboxes = CreateMemberCheckboxes(properties);
			FieldCheckboxes = CreateMemberCheckboxes(fields);
		}

		private static void FilterMembers(List<PropertyInfo> properties, List<FieldInfo> fields)
		{
			MemberFilter.FilterProperties(properties);
			MemberFilter.FilterFields(fields);
		}

		private static MemberCheckbox<T>[] CreateMemberCheckboxes<T>(IList<T> members) where T : MemberInfo
		{
			MemberCheckbox<T>[] fieldCheckBoxes = new MemberCheckbox<T>[members.Count];
			for (int i = 0; i < members.Count; ++i)
			{
				fieldCheckBoxes[i] = new MemberCheckbox<T>(members[i]);
			}
			return fieldCheckBoxes;
		}

		public void CopyTo(Component other)
		{
			foreach (MemberCheckbox<PropertyInfo> property in PropertyCheckboxes)
			{
				if (property.CanCopy())
				{
					PropertyInfo pi = property.Member;
					pi.SetValue(other, pi.GetValue(Component, null), null);
				}
			}

			foreach (MemberCheckbox<FieldInfo> field in FieldCheckboxes)
			{
				if (field.CanCopy())
				{
					FieldInfo fi = field.Member;
					fi.SetValue(other, fi.GetValue(Component));
				}
			}
		}

		private void DrawProperties()
		{
			for (int i = 0; i < PropertyCheckboxes.Length; ++i)
			{
				PropertyCheckboxes[i].Draw();
			}
		}

		private void DrawFields()
		{
			for (int i = 0; i < FieldCheckboxes.Length; ++i)
			{
				FieldCheckboxes[i].Draw();
			}
		}

		public void Draw()
		{
			string componentName = Component.GetType().Name;
			Texture icon = EditorGUIUtility.ObjectContent(Component, Component.GetType()).image;
			GUIContent content = new GUIContent(icon);
			content.text = componentName;

			Checked = EditorGUILayout.ToggleLeft(content, Checked, SmartCopier.MainLabelStyle);
			if (Checked)
			{
				m_Foldout = EditorGUILayout.Foldout(m_Foldout, componentName + " properties and fields");
				if (m_Foldout)
				{
					++EditorGUI.indentLevel;
					DrawProperties();
					DrawFields();
					--EditorGUI.indentLevel;
				}
			}
		}
	}
}
