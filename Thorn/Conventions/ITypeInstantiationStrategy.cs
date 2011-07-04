using System;

namespace Thorn.Conventions
{
	internal interface ITypeInstantiationStrategy
	{
		object Instantiate(Type type);
	}
}