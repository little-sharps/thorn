using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThornSample
{
	class Program
	{
		static void Main(string[] args)
		{
			Thorn.Runner.Configure(c => c.UseDashForSwitchDelimiter()).Run(args);
		}
	}
}
