using System;
using System.IO;
using System.Text;

namespace Thorn.Conventions
{
	class ArgsParameterHelpProvider : IParameterHelpProvider
	{
		private readonly ArgsHelper _argsHelper;

		public ArgsParameterHelpProvider(ArgsHelper argsHelper)
		{
			_argsHelper = argsHelper;
		}

		public string GetHelp(Type parameterType)
		{
			var sb = new StringBuilder();
			if (parameterType == null)
			{
				sb.AppendLine();
				sb.AppendLine("No Options.");
			}
			else
			{
				var provider = new Args.Help.HelpProvider();
				var bindingDefinition = _argsHelper.GetArgsModelBindingDefinitionForType(parameterType);
				var modelhelp = provider.GenerateModelHelp(bindingDefinition);

				var helpFormatter = new Args.Help.Formatters.ConsoleHelpFormatter(80, 1, 5);

				using (var writer = new StringWriter(sb))
				{
					helpFormatter.WriteHelp(modelhelp, writer);
					writer.Close();
				}
			}
			return sb.ToString();
		}
	}
}