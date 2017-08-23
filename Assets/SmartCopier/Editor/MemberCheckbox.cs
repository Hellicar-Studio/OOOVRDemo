using System.Reflection;
using UnityEditor;

namespace SmartCopier
{
	public class MemberCheckbox<T> where T : MemberInfo
	{
		public T Member { get; private set; }
		public bool Checked { get; set; }

		public MemberCheckbox(T member)
		{
			Member = member;
			Checked = IsAllowedToCopy();
		}

		public void Draw()
		{
			if (IsAllowedToCopy())
			{
				Checked = EditorGUILayout.ToggleLeft(Member.Name, Checked, SmartCopier.ListLabelStyle);
			}
		}

		private bool IsAllowedToCopy()
		{
			return !MemberFilter.HasNoCopyAttribute(Member);
		}

		public bool CanCopy()
		{
			return IsAllowedToCopy() && Checked;
		}
	}
}
