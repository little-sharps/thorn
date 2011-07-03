using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thorn
{
	internal static class Extensions
	{
		public static bool HasValue(this string s)
		{
			return !string.IsNullOrEmpty(s);
		}
	}
}
