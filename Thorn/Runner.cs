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
			try
			{
				return Builder.BuildRunner(configurator);
			}
			catch (ConfigurationException ex)
			{
				Console.WriteLine("Unable to create runner. A problem has been detected in your configuration:");
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
