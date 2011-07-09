using System;
using Thorn.Exceptions;

namespace Thorn
{
	internal class RunnerInternal : IRunner
	{
		private readonly HelpProvider _helpProvider;
		private readonly CommandProcessor _commandProcessor;

		public RunnerInternal(HelpProvider helpProvider, CommandProcessor commandProcessor)
		{
			_helpProvider = helpProvider;
			_commandProcessor = commandProcessor;
		}

		public void Run(string[] args)
		{
			try
			{
				var cmd = Command.Parse(args);

				if (_helpProvider.CanHandle(cmd))
				{
					_helpProvider.Handle(cmd);
				}
				else
				{
					_commandProcessor.Handle(cmd);
				}
			}
			catch (RoutingException)
			{
				Console.WriteLine("Unable to locate command");
				_helpProvider.PrintUsage();
			}
			catch (InvocationException)
			{
				Console.WriteLine("Unable to execute command");
				_helpProvider.PrintUsage();
			}
		}
	}
}