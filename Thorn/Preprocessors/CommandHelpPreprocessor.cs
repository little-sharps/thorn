using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thorn.Preprocessors
{
    public class CommandHelpPreprocessor : IPreprocessor
    {
    	private Configuration _config;

    	public CommandHelpPreprocessor(Configuration config)
    	{
    		_config = config;
    	}

    	public bool CanHandle(string[] args)
        {
            return args.Length > 1 && args[0].ToLower() == "help";
        }

        public void Handle(string[] args)
        {
            var commandName = args[1].ToLower();
            var router = new CommandRouter(_config);

            var action = router.FindAction(commandName);

            Console.WriteLine("Help - {0} - {1}", commandName, action.GetDescription());
            Console.WriteLine();

            Console.WriteLine(action.GetHelp());

            Console.WriteLine();
        }
    }
}
