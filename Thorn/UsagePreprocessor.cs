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

				Console.WriteLine();
				Console.WriteLine("Usage:");
				Console.WriteLine("  {0} [command] [/flag1 [flag1value]] [/flag2 [...]]...", executableName);
				Console.WriteLine();
				Console.WriteLine("{0} understands the following commands:", executableName);
				Console.WriteLine();

				foreach (var export in config.RoutingInfo.Exports)
				{
					if (export.Namespace == config.RoutingInfo.DefaultNamespace)
					{
						Console.WriteLine("  {0}", export.Name);
					}
					else
					{
						Console.WriteLine("  {0}:{1}", export.Namespace, export.Name);
					}

					if (export.Description.HasValue())
					{
						Console.WriteLine("\t{0}", export.Description);
					}
				}

				Console.WriteLine();
				Console.WriteLine("For info on a particular command, try: '{0} help <command>'", executableName);

				return true;
			}

			return false;
		}
	}
}
