using System;
using Microsoft.Practices.ServiceLocation;

namespace Thorn.Conventions
{
	internal class CommonServiceLocatorInstantiationStrategy : ITypeInstantiationStrategy
	{
		public object Instantiate(Type type)
		{
			return ServiceLocator.Current.GetInstance(type);
		}
	}
}