using System;
using System.Linq;

namespace Thorn.Config
{
	internal interface ITypeSource
	{
		IQueryable<Type> Types { get; }
	}
}