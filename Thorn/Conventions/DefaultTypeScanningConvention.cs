using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Thorn.Conventions
{
	public class DefaultTypeScanningConvention : ITypeScanningConvention
	{
		public virtual IQueryable<Type> TypesToExport(IQueryable<Type> typesToScan)
		{
			return from type in typesToScan
			       where type.GetCustomAttributes(false).Any(attr => attr is ThornExportAttribute)
			       select type;
		}

		public virtual IEnumerable<MethodInfo> MethodsToExport(Type type)
		{
			return type.GetMethods().AsQueryable()
				.Where(m => m.DeclaringType == type)
				.Where(
					m => !(m.GetCustomAttributes(false).AsQueryable().Any(attr => attr is ThornIgnoreAttribute))
				);
		}

		public virtual string GetNamespace(Type type, MethodInfo methodInfo)
		{
			return type.Name.ToLower();
		}

		public virtual string GetName(Type type, MethodInfo methodInfo)
		{
			return methodInfo.Name.ToLower();
		}

		public virtual string GetDescription(Type type, MethodInfo methodInfo)
		{
			DescriptionAttribute descriptionAttr = null;
			foreach (var attribute in methodInfo.GetCustomAttributes(typeof(DescriptionAttribute), false))
			{
				descriptionAttr = attribute as DescriptionAttribute;
			}

			return descriptionAttr == null ? null : descriptionAttr.Description;
		}

	}
}