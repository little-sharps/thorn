using System;
using System.Reflection;
using Thorn.Conventions;

namespace Thorn
{
	class HelpProvider
	{
		private readonly CommandRouter _router;
		private readonly IParameterHelpProvider _parameterHelpProvider;
		private readonly string _executableName = Assembly.GetEntryAssembly().GetName().Name;

		public HelpProvider(CommandRouter router, IParameterHelpProvider parameterHelpProvider)
		{
			_router = router;
			_parameterHelpProvider = parameterHelpProvider;
		}

		public bool CanHandle(Command cmd)
		{
			return cmd.CommandString == null || cmd.CommandString == "help";
		}

		public void Handle(Command cmd)
		{
			if (cmd.CommandString == "help" && cmd.Args.Length > 0)
			{
				var referencedCommand = Command.Parse(cmd.Args);
				PrintCommandHelp(referencedCommand);
			}
			else
			{
				PrintUsage();
			}
		}

		public void PrintCommandHelp(Command cmd)
		{
			var export = _router.FindExport(cmd);

			Console.WriteLine("Help - {0}:{1}", export.Namespace, export.Name);
			if (export.Description.HasValue())
			{
				Console.WriteLine("\t{0}", export.Description);
			}
			Console.Write(_parameterHelpProvider.GetHelp(export.ParameterType));
		}

		public void PrintUsage()
		{
			Console.WriteLine();
			Console.WriteLine("Usage:");
			Console.WriteLine("  {0} [command] [/flag1 [flag1value]] [/flag2 [...]]...", _executableName);
			Console.WriteLine();
			Console.WriteLine("{0} understands the following commands:", _executableName);
			Console.WriteLine();

			foreach (var export in _router.RoutingInfo.Exports)
			{
				if (export.Namespace == _router.RoutingInfo.DefaultNamespace)
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
			Console.WriteLine("For info on a particular command, try: '{0} help <command>'", _executableName);
		}

		public void PrintHint()
		{
			Console.WriteLine();
			Console.WriteLine("For more info, try: '{0} help'", _executableName);
		}
	}
}
