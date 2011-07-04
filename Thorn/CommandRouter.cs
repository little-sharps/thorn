using System;
using System.Linq;
using Thorn.Config;
using Thorn.Exceptions;

namespace Thorn
{
	internal class CommandRouter
	{
		private readonly RoutingInfo _routes;

		public CommandRouter(RoutingInfo routes)
		{
			_routes = routes;
		}

		public Export FindExport(string commandString)
		{
			var command = Command.Parse(commandString);

			try
			{
				var @namespace = command.Namespace.HasValue() ? command.Namespace : _routes.DefaultNamespace;
				return _routes.ExportsInNamespace(@namespace).Single(export => export.Name == command.ActionName);
			}
			catch (Exception)
			{
				throw new RoutingException(commandString);
			}
		}

	}
}