using System;
using Thorn.Config;
using Thorn.Exceptions;

namespace Thorn
{
	internal class RunnerInternal : IRunner
	{
		private readonly Configuration _config;

		internal RunnerInternal(Configuration config)
		{
			_config = config;
		}

		public void Run(string[] args)
		{
			try
			{
				_config.AssertConfigurationIsValid();
				var handled = new UsagePreprocessor().Handle(_config, args);
				if (!handled) handled = new CommandHelpPreprocessor().Handle(_config, args);
				if (!handled) new CommandProcessor().Handle(_config, args);
			}
			catch (RoutingException ex)
			{
				Console.WriteLine("Command not found - {0}", ex.Command);
			}

		}
	}
}