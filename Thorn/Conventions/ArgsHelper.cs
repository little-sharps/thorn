using System;
using System.Linq;
using System.Reflection;
using Args;

namespace Thorn.Conventions
{
	internal static class ArgsHelper
	{
		public static dynamic GetArgsModelBindingDefinitionForType(Type type)
		{
			var genericConfigureMethod = typeof(Configuration).GetMethods(BindingFlags.Static | BindingFlags.Public).AsQueryable()
				.First(m => m.Name == "Configure" && m.GetParameters().Count() == 0);

			var closedConfigureMethod = genericConfigureMethod.MakeGenericMethod(type);

			return closedConfigureMethod.Invoke(null, new object[0]);
		}
	}
}
