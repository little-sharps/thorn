using System;
using System.Linq;
using System.Reflection;
using Args;

namespace Thorn.Conventions
{
	internal class ArgsHelper
	{
		private readonly string _switchDelimiter;

		public ArgsHelper(string switchDelimiter)
		{
			_switchDelimiter = switchDelimiter;
		}

		public dynamic GetArgsModelBindingDefinitionForType(Type type)
		{
			var genericConfigureMethod = typeof(Configuration).GetMethods(BindingFlags.Static | BindingFlags.Public).AsQueryable()
				.First(m => m.Name == "Configure" && m.GetParameters().Count() == 0);

			var closedConfigureMethod = genericConfigureMethod.MakeGenericMethod(type);

			dynamic bindingDefinition = closedConfigureMethod.Invoke(null, new object[0]);

			if (_switchDelimiter.HasValue())
			{
				bindingDefinition.SwitchDelimiter = _switchDelimiter;
			}

			return bindingDefinition;
		}
	}
}
