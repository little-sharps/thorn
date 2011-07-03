using System;
using System.Collections.Generic;
using System.Linq;

namespace Thorn
{
	public interface ITypeSource
	{
		IQueryable<Type> Types { get; }
	}
}