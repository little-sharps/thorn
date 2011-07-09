using System;

namespace Thorn.Exceptions
{
	public class RoutingException : Exception
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