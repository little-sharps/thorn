using System.Collections.Generic;
using Thorn.Conventions;

namespace Thorn
{
	internal class CommandProcessor
	{
		private readonly CommandRouter _router;
		private readonly ITypeInstantiationStrategy _instantiationStrategy;
		private readonly IParameterBinder _parameterBinder;

		public CommandProcessor(CommandRouter router, ITypeInstantiationStrategy instantiationStrategy, IParameterBinder parameterBinder)
		{
			_router = router;
			_parameterBinder = parameterBinder;
			_instantiationStrategy = instantiationStrategy;
		}

		public void Handle(Command cmd)
		{
			var export = _router.FindExport(cmd);

			var target = _instantiationStrategy.Instantiate(export.Type);
			
			var parameters = new List<object>();
			if (export.ParameterType != null)
			{
				parameters.Add(_parameterBinder.BuildParameter(export.ParameterType, cmd.Args));
			}
			
			export.Method.Invoke(target, parameters.ToArray());
		}
	}
}
