using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Thorn
{
	public class DefaultMemberScanningConvention : IMemberScanningConvention
	{
		public IEnumerable<Action> GetActions(Type type)
		{
			return type.GetMethods().AsQueryable()
				.Where(m => m.DeclaringType == type)
				.Where(
					m => !(m.GetCustomAttributes(false).AsQueryable().Any(attr => attr is ThornIgnoreAttribute))
				).Select(mi => new Action(type, mi, GetName(type, mi)));
		}

		private string GetName(Type type, MethodInfo mi)
		{
			return mi.Name.ToLower();
		}
	}
}