using System;
using System.Collections.Generic;
using System.Linq;
using Thorn.Preprocessors;

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
			var commandHasBeenProcessed = false;

			try
			{
				PreprocessCommand(args, () => { commandHasBeenProcessed = true; });

				if (!commandHasBeenProcessed)
				{
					ProcessCommand(args);
				}
			}
			catch (RoutingException ex)
			{
				Console.WriteLine("Command not found - {0}", ex.Command);
			}

		}

		private void PreprocessCommand(string[] args, System.Action success)
		{
			foreach (var preprocessor in GetPreprocessors())
			{
				if (preprocessor.CanHandle(args))
				{
					preprocessor.Handle(args);
					success();
					break;
				}
			}
		}

		private void ProcessCommand(string[] args)
		{
			var command = args[0].ToLower();
			args = args.Skip(1).ToArray();

			var router = new CommandRouter(_config);
			var action = router.FindAction(command);

			var target = _config.InstantiationStrategy.Instantiate(action.Type);
			action.Invoke(target, args);
		}
		
		private IEnumerable<IPreprocessor> GetPreprocessors()
		{
			return new IPreprocessor[]
			       {
			       	new IndexPreprocessor(_config),
			       	new CommandHelpPreprocessor(_config)
			       };
		}
	}
}