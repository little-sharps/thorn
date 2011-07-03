using System;

namespace Thorn
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class ThornExportAttribute : Attribute
	{
		public bool IsDefault { get; set; }
	}
}