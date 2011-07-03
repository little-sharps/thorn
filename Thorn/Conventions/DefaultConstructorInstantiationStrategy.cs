using System;

namespace Thorn.Conventions
{
	internal class DefaultConstructorInstantiationStrategy : ITypeInstantiationStrategy
	{
		public object Instantiate(Type type)
		{
			return Activator.CreateInstance(type);
		}
	}
}