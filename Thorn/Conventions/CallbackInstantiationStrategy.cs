using System;

namespace Thorn.Conventions
{
	internal class CallbackInstantiationStrategy : ITypeInstantiationStrategy
	{
		private Func<Type,object> _callback;

		public CallbackInstantiationStrategy(Func<Type,object> callback)
		{
			_callback = callback;
		}

		public object Instantiate(Type type)
		{
			return _callback(type);
		}
	}
}
