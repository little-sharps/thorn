using System.Linq;

namespace Thorn
{
	internal class Command
	{
		private string _commandString;
		private string _ns;
		private string _name;
		private string[] _args;

		public Command(string commandString, string ns, string name, string[] args)
		{
			_commandString = commandString;
			_ns = ns;
			_name = name;
			_args = args;
		}

		public string CommandString
		{
			get { return _commandString; }
		}

		public string Namespace
		{
			get { return _ns; }
		}

		public string Name
		{
			get { return _name; }
		}

		public string[] Args
		{
			get { return _args; }
		}

		public static Command Parse(string[] rawargs)
		{
			string commandString = null;
			string ns = null;
			string name = null;
			var args = new string[0];

			if (rawargs.Length > 0)
			{
				commandString = rawargs.First().ToLower();
				args = rawargs.Skip(1).ToArray();

				if (commandString.Contains(":"))
				{
					var parts = commandString.Split(':');
					ns = parts[0];
					name = parts[1];
				}
				else
				{
					name = commandString;
				}
			}
			return new Command(commandString, ns, name, args);
		}
	}
}