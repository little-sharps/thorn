using System;

namespace Thorn
{
	public class CommandRouter
	{
		private Configuration _config;

		public CommandRouter(Configuration config)
		{
			_config = config;
		}

		public Action FindAction(string commandString)
		{
			var command = Command.Parse(commandString);

			try
			{
				var @namespace = command.Namespace.HasValue() ? command.Namespace : _config.DefaultNamespace;
				return _config.GetExportByNamespace(@namespace).GetActionByName(command.ActionName);
			}
			catch (Exception)
			{
				throw new RoutingException(commandString);
			}
		}

	}
}