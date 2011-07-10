using System;
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

			object target = null;
			
			try
			{
				target = _instantiationStrategy.Instantiate(export.Type);
			}
			catch(Exception ex)
			{
				var msg = string.Format("Unable to instantiate type {0}", export.Type.FullName);
				throw new InvocationException(msg, ex);
			}
			
			var parameters = new List<object>();
			if (export.ParameterType != null)
			{
				try
				{
					parameters.Add(_parameterBinder.BuildParameter(export.ParameterType, cmd.Args));
				}
				catch (Exception ex)
				{
					var msg = string.Format("Unable to bind parameter type {0}", export.ParameterType.FullName);
					throw new InvocationException(msg, ex);
				}
			}
			
			export.Method.Invoke(target, parameters.ToArray());
		}
	}
}
