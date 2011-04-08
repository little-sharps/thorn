using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thorn.Preprocessors;

namespace Thorn
{
    public class Runner
    {
        public static void Run(string[] args)
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

        private static void PreprocessCommand(string[] args, System.Action success)
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

        private static void ProcessCommand(string[] args)
        {
            var command = args[0].ToLower();
            args = args.Skip(1).ToArray();

            var router = new CommandRouter(Configuration.GetConfiguredActions(), Configuration.GetDefaultReceiver());
            var action = router.FindAction(command);

            action.Invoke(args);
        }
        
        private static IEnumerable<IPreprocessor> GetPreprocessors()
        {
            return new IPreprocessor[]
            {
                new IndexPreprocessor(),
                new CommandHelpPreprocessor()
            };
        }
    }
}
