using System;
using System.Reflection;
using Thorn.Config;

namespace Thorn
{
	internal class UsagePreprocessor
	{
		public bool Handle(Configuration config, string[] args)
		{
			if (args.Length == 0 || (args.Length == 1 && args[0].ToLower() == "help"))
			{
				var executableName = Assembly.GetEntryAssembly().GetName().Name;

				Console.WriteLine("Usage:");
				Console.WriteLine("  {0} [command] [/flag1 [flag1value]] [/flag2 [...]]...", executableName);
				Console.WriteLine();
				Console.WriteLine("{0} understands the following commands:", executableName);

				foreach (var export in config.RoutingInfo.Exports)
				{
					Console.WriteLine("  {0}:{1}\t{2}", export.Namespace, export.Name, export.Description);
				}

				Console.WriteLine();
				Console.WriteLine("For info on a particular command, try:");
				Console.WriteLine("  {0} help <command>", executableName);
				Console.WriteLine();

				return true;
			}

			return false;
		}
	}
}
