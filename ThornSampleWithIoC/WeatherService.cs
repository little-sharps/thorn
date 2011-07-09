namespace ThornSampleWithIoC
{
	public interface IWeatherService
	{
		decimal GetCurrentTempAtZipCode(string zip);
	}

	public class WeatherService : IWeatherService
	{
		public decimal GetCurrentTempAtZipCode(string zip)
		{
			return 67.2M;
		}
	}
}