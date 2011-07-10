using System;

namespace Thorn
{
	internal class ConfigurationException : Exception 
	{
		public ConfigurationException(string s) : base(s) {}
	}

	internal class InvocationException : Exception
	{
		public InvocationException(string s) : base(s) { }
		public InvocationException(string s, Exception inner) : base(s, inner) { }
	}

	internal class RoutingException : Exception
	{
		private string _command;

		public RoutingException(string command)
		{
			_command = command;
		}

		public string Command
		{
			get { return _command; }
		}
	}
}