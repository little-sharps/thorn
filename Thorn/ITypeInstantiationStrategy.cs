using System;

namespace Thorn
{
	public interface ITypeInstantiationStrategy
	{
		object Instantiate(Type type);
	}
}