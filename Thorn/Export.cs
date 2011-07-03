using System;
using System.Collections.Generic;

namespace Thorn
{
	public class Export
	{
		private Type _type;
		private bool _isDefault;
		private string _namespace;
		private readonly IDictionary<string, Action> _actions = new Dictionary<string, Action>();

		public Export(Type type, bool isDefault, string @namespace, IEnumerable<Action> actions)
		{
			_type = type;
			_isDefault = isDefault;
			_namespace = @namespace;
			foreach (var action in actions)
			{
				AddAction(action);
			}
		}

		public Type Type
		{
			get { return _type; }
		}

		public bool IsDefault
		{
			get { return _isDefault; }
		}

		public string Namespace
		{
			get { return _namespace; }
		}

		public IEnumerable<Action> Actions
		{
			get { return _actions.Values; }
		}

		private void AddAction(Action action)
		{
			_actions[action.Name] = action;
		}

		public Action GetActionByName(string actionName)
		{
			return _actions[actionName];
		}
	}
}