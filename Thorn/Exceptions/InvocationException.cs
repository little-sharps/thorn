using System;

namespace Thorn.Exceptions
{
	public class InvocationException : Exception
	{
		public InvocationException(string s) : base(s) { }
	}
}