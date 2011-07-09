using StructureMap;

namespace ThornSampleWithIoC
{
	class Program
	{
		static void Main(string[] args)
		{
			ConfigureStructureMap();
			Thorn.Runner
				.Configure(config =>
					{
						config.UseCallbackToInstantiateExports(ObjectFactory.GetInstance);
					})
				.Run(args);
		}

		static void ConfigureStructureMap()
		{
			ObjectFactory.Initialize(config => config.For<IWeatherService>().Use<WeatherService>());
		}
	}
}
