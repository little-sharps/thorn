using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Thorn.Preprocessors
{
    public class IndexPreprocessor : IPreprocessor
    {
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

            var actions = Configuration.GetConfiguredActions();

            foreach (var action in actions)
            {
                Console.WriteLine("\t{0}:{1}\t{2}", action.Type.Name.ToLower(), action.Method.Name.ToLower(), action.GetDescription());
            }

            Console.WriteLine();
            Console.WriteLine("For info on a particular command, try:");
            Console.WriteLine("\t{0} help <command>", executableName);
            Console.WriteLine();
        }
    }
}
