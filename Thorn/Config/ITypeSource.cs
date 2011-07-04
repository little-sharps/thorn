using System;
using System.Linq;

namespace Thorn.Config
{
	public interface ITypeSource
	{
		IQueryable<Type> Types { get; }
	}
}