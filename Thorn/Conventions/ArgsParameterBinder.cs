using System;

namespace Thorn.Conventions
{
	internal class ArgsParameterBinder : IParameterBinder
	{
		private readonly ArgsHelper _argsHelper;

		public ArgsParameterBinder(ArgsHelper argsHelper)
		{
			_argsHelper = argsHelper;
		}

		public object BuildParameter(Type type, string[] args)
		{
			var bindingDefinition = _argsHelper.GetArgsModelBindingDefinitionForType(type);
			return bindingDefinition.CreateAndBind(args);
		}
	}
}