using System;
using Thorn.Config;

namespace Thorn
{
	public class Runner
	{
		public static void Run(string[] args)
		{
			Configure().Run(args);
		}

		public static IRunner Configure(Action<IConfigurationHelper> configurator = null)
		{
			return new RunnerInternal(ConfigurationBuilder.Build(configurator));
		}
	}
}
