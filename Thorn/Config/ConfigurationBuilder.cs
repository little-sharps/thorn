using System;
using System.Collections.Generic;
using Thorn.Conventions;

namespace Thorn.Config
{
	internal class ConfigurationBuilder
	{
		public static Configuration Build(Action<IConfigurationHelper> configurator)
		{
			var configPlan = new ConfigurationPlan();
			
			if (configurator != null)
			{
				configurator(configPlan);
			}
			
			return Build(configPlan);
		}

		private static Configuration Build(ConfigurationPlan configPlan)
		{
			var config = new Configuration();

			var exports = new List<Export>();
			var typeScanner = new TypeScanner(configPlan.TypeScanningConvention);

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

			config.RoutingInfo = new RoutingInfo(exports, configPlan.DefaultNamespace);

			config.InstantiationStrategy = configPlan.TypeInstantiationStrategy;
			config.ParameterHelpProvider = new ArgsHelpProvider();
			config.ParameterBinder = new ArgsParameterBinder();

			return config;
		}

		
	}
}
