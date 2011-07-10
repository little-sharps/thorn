using System;
using System.Linq;
using Thorn.Config;

namespace Thorn
{
	internal class CommandRouter
	{
		private readonly RoutingInfo _routingInfo;

		public CommandRouter(RoutingInfo routingInfo)
		{
			_routingInfo = routingInfo;
		}

		public RoutingInfo RoutingInfo
		{
			get { return _routingInfo; }
		}

		public Export FindExport(Command command)
		{
			try
			{
				var @namespace = command.Namespace.HasValue() ? command.Namespace : _routingInfo.DefaultNamespace;
				return _routingInfo.ExportsInNamespace(@namespace).Single(export => export.Name == command.Name);
			}
			catch (Exception)
			{
				throw new RoutingException(command.CommandString);
			}
		}

	}
}