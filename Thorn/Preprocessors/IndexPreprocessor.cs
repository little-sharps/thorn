using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Thorn.Preprocessors
{
	public class IndexPreprocessor : IPreprocessor
	{
		private Configuration _config;

		public IndexPreprocessor(Configuration config)
		{
			_config = config;
		}

		public bool CanHandle(string[] args)
		{
			return args.Length == 0 || 
				  (args.Length == 1 && args[0].ToLower() == "help");
		}

		public void Handle(string[] args)
		{
			var executableName = Assembly.GetEntryAssembly().GetName().Name;

			Console.WriteLine("Usage:");
			Console.WriteLine("\t{0} [command] [/flag1 [flag1value]] [/flag2 [...]]...", executableName);
			Console.WriteLine();
			Console.WriteLine("{0} understands the following commands:", executableName);

			var targets = from export in _config.Exports
			              from action in export.Actions
			              select new {export, action};

			foreach (var target in targets)
			{
				Console.WriteLine("\t{0}:{1}\t{2}", target.export.Namespace, target.action.Name, target.action.GetDescription());
			}

			Console.WriteLine();
			Console.WriteLine("For info on a particular command, try:");
			Console.WriteLine("\t{0} help <command>", executableName);
			Console.WriteLine();
		}
	}
}
