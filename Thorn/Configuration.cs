using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Thorn
{
    public class Configuration
    {
        public static ICommandRouter Configure()
        {
            var scanner = new AssemblyScanner();
            return new CommandRouter(scanner.GetActionsFrom(Assembly.GetEntryAssembly()));
        }

        public static ICommandRouter Configure(Assembly assembly)
        {
            var scanner = new AssemblyScanner();
            return new CommandRouter(scanner.GetActionsFrom(assembly));
        }

        public static ICommandRouter Configure(Assembly assembly, string @namespace)
        {
            var scanner = new AssemblyScanner();
            return new CommandRouter(scanner.GetActionsFrom(assembly, @namespace));
        }


    }
}