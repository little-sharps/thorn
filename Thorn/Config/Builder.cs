using System;
using System.Collections.Generic;
using Thorn.Conventions;

namespace Thorn.Config
{
	internal class Builder
	{
		public static IRunner BuildRunner(Action<IConfigurationHelper> configurator)
		{
			var configPlan = new ConfigurationPlan();

			if (configurator != null)
			{
				configurator(configPlan);
			}

			configPlan.Validate();

			var routingInfo = GetRoutingInfo(configPlan);

			routingInfo.Validate();

			var router = new CommandRouter(routingInfo);
			
			var argsHelper = new ArgsHelper(configPlan.SwitchDelimiter);
			var argsHelpProvider = new ArgsParameterHelpProvider(argsHelper);
			var argsParameterBinder = new ArgsParameterBinder(argsHelper);

			var helpProvider = new HelpProvider(router, argsHelpProvider);
			var cmdProcessor = new CommandProcessor(router, configPlan.TypeInstantiationStrategy, argsParameterBinder);

			return new RunnerInternal(helpProvider, cmdProcessor);
		}

		private static RoutingInfo GetRoutingInfo(ConfigurationPlan configPlan)
		{
			var exports = new List<Export>();
			var typeScanner = new TypeScanner(configPlan.TypeScanningConvention);
			string defaultNamespace = null;

			foreach (var source in configPlan.TypeSources)
			{
				exports.AddRange(typeScanner.GetExportsIn(source));
			}

			foreach (var type in configPlan.AdditionalTypes)
			{
				exports.AddRange(typeScanner.GetExportsIn(type));
			}

			foreach (var export in configPlan.AdditionalExports)
			{
				exports.Add(export);
			}

			if (configPlan.DefaultType != null)
			{
				defaultNamespace = configPlan.TypeScanningConvention.GetNamespace(configPlan.DefaultType);
			}

			return new RoutingInfo(exports, defaultNamespace);
		}
	}
}
