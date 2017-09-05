using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SmartCopier
{
	public class MemberFilter
	{
		private static readonly List<PropertyInfo> m_FilterProperties = new List<PropertyInfo>();

		private static void CreateFilters()
		{
			if (m_FilterProperties.Count == 0)
			{
				Type componentType = typeof(MonoBehaviour);
				m_FilterProperties.AddRange(componentType.GetProperties(SmartCopier.Flags));
			}
		}

		public static bool HasNoCopyAttribute(MemberInfo member)
		{
			return member.GetCustomAttributes(typeof (NoCopyAttribute), false).Length > 0;
		}

		private static bool CanFilterProperty(PropertyInfo property)
		{
			return !property.CanWrite ||
			       m_FilterProperties.Any(prop => prop.MetadataToken == property.MetadataToken);
		}

		private static bool CanFilterField(FieldInfo field)
		{
			object[] serializeFieldAttributes = field.GetCustomAttributes(typeof (SerializeField), false);
			return !field.IsPublic && serializeFieldAttributes.Length == 0;
		}

		public static void FilterProperties(List<PropertyInfo> properties)
		{
			CreateFilters();
			properties.RemoveAll(CanFilterProperty);
		}

		public static void FilterFields(List<FieldInfo> fields)
		{
			fields.RemoveAll(CanFilterField);
		}
	}
}
