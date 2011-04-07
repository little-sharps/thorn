using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Thorn
{
    public class CommandRouter : ICommandRouter
    {
        private IList<CommandRoute> _routes = new List<CommandRoute>();

        public CommandRouter(IEnumerable<IAction> actions)
        {
            foreach (var action in actions)
            {
                _routes.Add(new CommandRoute(action));
            }
        }

        public IAction FindAction(string command)
        {
            var parts = command.Split(':');
            CommandRoute route;
            
            if (parts.Count() > 1)
            {
                route = _routes.AsQueryable().Where(r => r.CommandName == parts[1] && r.Scope == parts[0]).First();
            }
            else
            {
                route = _routes.AsQueryable().Where(r => r.CommandName == parts[0]).First();
            }
            
            return route.Action;
        }

        private class CommandRoute
        {
            private readonly string _scope;
            private readonly string _commandName;
            private readonly IAction _action;

            public CommandRoute(IAction action)
            {
                _scope = action.Type.Name.ToLower();
                _commandName = action.Method.Name.ToLower();
                _action = action;
            }

            public string Scope
            {
                get { return _scope; }
            }

            public string CommandName
            {
                get { return _commandName; }
            }

            public IAction Action
            {
                get { return _action; }
            }
        }
    }
}