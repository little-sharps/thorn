using System;
using System.IO;
using System.Text;

namespace Thorn.Conventions
{
	class ArgsHelpProvider : IParameterHelpProvider
	{
		public string GetHelp(Type parameterType)
		{
			var modelhelp =
				new Args.Help.HelpProvider().GenerateModelHelp(
					ArgsHelper.GetArgsModelBindingDefinitionForType(parameterType));

			var helpFormatter = new Args.Help.Formatters.ConsoleHelpFormatter(80, 1, 5);

			var sb = new StringBuilder();
			using (var writer = new StringWriter(sb))
			{
				helpFormatter.WriteHelp(modelhelp, writer);
				writer.Close();
			}
			return sb.ToString();
		}
	}
}