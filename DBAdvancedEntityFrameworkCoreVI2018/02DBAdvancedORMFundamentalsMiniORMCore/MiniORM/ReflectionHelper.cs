﻿using System;
using System.Linq;
using System.Reflection;

namespace MiniORM
{
	internal static class ReflectionHelper
	{
		/// <summary>
		/// Replaces an auto-generated backing field with an object instance.
		/// Commonly used to set properties without a setter.
		/// </summary>
		public static void ReplaceBackingField(object sourceObject, string propertyName, object targetObject)
		{
			FieldInfo backingField = sourceObject.GetType()
				.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField)
				.First(fi => fi.Name == $"<{propertyName}>k__BackingField");

			backingField.SetValue(sourceObject, targetObject);
		}

		/// <summary>
		/// Extension method for MemberInfo, which checks if a member contains an attribute.
		/// </summary>
		public static bool HasAttribute<T>(this MemberInfo memberInfo) where T : Attribute
		{
			bool hasAttribute = memberInfo.GetCustomAttribute<T>() != null;

			return hasAttribute;
		}
	}
}