using System;

namespace Thorn.Conventions
{
	public interface ITypeInstantiationStrategy
	{
		object Instantiate(Type type);
	}
}