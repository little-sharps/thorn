using System;

namespace Thorn
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
			
			var typeScanner = configPlan.TypeScanningConvention;
			var memberScanner = configPlan.MemberScanningConvention;

			foreach (var source in configPlan.TypeSources)
			{
				foreach (var export in typeScanner.GetExports(source.Types, memberScanner))
				{
					config.AddExport(export);
				}
			}

			foreach (var type in configPlan.AdditionalExports)
			{
				config.AddExport(new Export(type, false, type.Name, memberScanner.GetActions(type)));
			}

			config.InstantiationStrategy = configPlan.TypeInstantiationStrategy;
			config.DefaultNamespace = configPlan.DefaultNamespace;

			return config;
		}
	}
}
