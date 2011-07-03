using System;

namespace Thorn
{
	internal class DefaultConstructorInstantiationStrategy : ITypeInstantiationStrategy
	{
		public object Instantiate(Type type)
		{
			return Activator.CreateInstance(type);
		}
	}
}