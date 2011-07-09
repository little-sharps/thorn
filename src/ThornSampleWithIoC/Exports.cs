using System;
using System.ComponentModel;
using Thorn;

namespace ThornSampleWithIoC
{
	[ThornExport]
	public class Exports
	{
		private IWeatherService _weatherService;

		public Exports(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		[Description("Gets the current temp in the given zip code")]
		public void Temp(Location location)
		{
			var temp = _weatherService.GetCurrentTempAtZipCode(location.Zip);
			Console.WriteLine("The current temp in zip code {0} is {1} degrees.", location.Zip, temp);
		}
	}

	public class Location
	{
		[Description("US Postal (Zip) Code")]
		public string Zip { get; set; }
	}
}
