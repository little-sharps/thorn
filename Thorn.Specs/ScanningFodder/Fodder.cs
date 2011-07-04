using System.ComponentModel;

namespace Thorn.Specs.ScanningFodder
{
	[ThornExport]
	public class Fodder
	{
		[Description("Bantha Description")]
		public void Bantha()
		{
			
		}

		public void Hey(HeyOptions opts)
		{
			
		}

		[ThornIgnore]
		public void Ignored()
		{
			
		}

		public class HeyOptions
		{
			public string ToWhom { get; set; }
		}
	}
}
