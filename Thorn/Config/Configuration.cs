using Thorn.Conventions;
using Thorn.Exceptions;

namespace Thorn.Config
{
	internal class Configuration
	{
		public RoutingInfo RoutingInfo { get; set; }

		public ITypeInstantiationStrategy InstantiationStrategy { get; set; }
		public IParameterHelpProvider ParameterHelpProvider { get; set; }
		public IParameterBinder ParameterBinder { get; set; }

		public void AssertConfigurationIsValid()
		{
			if(InstantiationStrategy == null)
				throw new ConfigurationException("An InstantiationStrategy must be configured.");

			if(ParameterBinder == null)
				throw new ConfigurationException("A ParameterBinder must be configured.");

			if(ParameterHelpProvider == null)
				throw new ConfigurationException("A ParameterHelpProvider must be configured.");

			if(RoutingInfo == null)
				throw new ConfigurationException("RoutingInfo must be configured.");

			RoutingInfo.AssertConfigurationIsValid();
		}
	}
}