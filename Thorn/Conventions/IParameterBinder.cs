using System;

namespace Thorn.Conventions
{
	public interface IParameterBinder
	{
		object BuildParameter(Type parameterType, string[] args);
	}
}
