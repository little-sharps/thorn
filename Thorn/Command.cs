namespace Thorn
{
	internal class Command
	{
		private readonly string _ns;
		private readonly string _actionName;

		public Command(string @namespace, string actionName)
		{
			_ns = @namespace;
			_actionName = actionName;
		}

		public string Namespace
		{
			get { return _ns; }
		}

		public string ActionName
		{
			get { return _actionName; }
		}

		public static Command Parse(string commandString)
		{
			string @namespace = null;
			string actionName;

			commandString = commandString.ToLower();

			if (commandString.Contains(":"))
			{
				var parts = commandString.Split(':');
				@namespace = parts[0];
				actionName = parts[1];
			}
			else
			{
				actionName = commandString;
			}

			return new Command(@namespace, actionName);
		}
	}
}