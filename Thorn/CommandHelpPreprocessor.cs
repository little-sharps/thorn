using System;
using Thorn.Config;

namespace Thorn
{
	internal class CommandHelpPreprocessor
	{
		public bool Handle(Configuration configuration, string[] args)
		{
			if (args.Length > 1 && args[0].ToLower() == "help")
			{
				var commandName = args[1].ToLower();
				var router = new CommandRouter(configuration.RoutingInfo);
				var action = router.FindExport(commandName);

				Console.WriteLine("Help - {0} - {1}", commandName, action.Description);
				Console.Write(configuration.ParameterHelpProvider.GetHelp(action.ParameterType));
				
				return true; //handled
			}

			return false;
		}
	}
}
