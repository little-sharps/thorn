using System;

namespace Thorn.Exceptions
{
	public class ConfigurationException : Exception 
	{
		public ConfigurationException(string s) : base(s) {}
	}
}