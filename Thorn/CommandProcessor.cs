using System.Collections.Generic;
using System.Linq;
using Thorn.Config;

namespace Thorn
{
	internal class CommandProcessor
	{
		public void Handle(Configuration config, string[] args)
		{
			var command = args[0].ToLower();
			args = args.Skip(1).ToArray();

			var router = new CommandRouter(config.RoutingInfo);
			var export = router.FindExport(command);

			var target = config.InstantiationStrategy.Instantiate(export.Type);
			
			var parameters = new List<object>();
			if (export.ParameterType != null)
			{
				parameters.Add(config.ParameterBinder.BuildParameter(export.ParameterType, args));
			}
			
			export.Method.Invoke(target, parameters.ToArray());
		}
	}
}
