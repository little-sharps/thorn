using System;
using System.Collections.Generic;
using System.Linq;

namespace Thorn
{
    public class CommandRouter
    {
        private IList<CommandRoute> _routes = new List<CommandRoute>();
        private Type _defaultReceiver;

        public CommandRouter(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                _routes.Add(new CommandRoute(action));
            }
        }

        public CommandRouter(IEnumerable<Action> actions, Type defaultReceiver) : this(actions)
        {
            _defaultReceiver = defaultReceiver;
        }

        public Action FindAction(string command)
        {
            RoutingInfo parsedRoutingInfo = ParseRoutingInfo(command);

            foreach (var route in _routes)
            {
                if (route.RoutingInfo == parsedRoutingInfo)
                {
                    return route.Action;
                }
            }

            throw new RoutingException(command);
        }

        private RoutingInfo ParseRoutingInfo(string command)
        {
            string scope, target;

            if (command.Contains(':'))
            {
                var parts = command.Split(':');
                scope = parts[0];
                target = parts[1];
            }
            else
            {
                scope = _defaultReceiver == null ? String.Empty : _defaultReceiver.Name;
                target = command;
            }

            return new RoutingInfo(scope, target);
        }

        private class CommandRoute
        {
            private readonly Action _action;
            private readonly RoutingInfo _routingInfo;

            public CommandRoute(Action action)
            {
                _action = action;
                _routingInfo = action.GetRoutingInfo();
            }

            public RoutingInfo RoutingInfo
            {
                get { return _routingInfo; }
            }

            public Action Action
            {
                get { return _action; }
            }
        }
    }
}