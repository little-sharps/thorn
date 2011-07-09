using System;

namespace Thorn.Conventions
{
	internal class ArgsParameterBinder : IParameterBinder
	{
		public object BuildParameter(Type type, string[] args)
		{
			return ArgsHelper.GetArgsModelBindingDefinitionForType(type).CreateAndBind(args);
		}
	}
}