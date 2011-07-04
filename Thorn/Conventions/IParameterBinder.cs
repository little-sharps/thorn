using System;

namespace Thorn.Conventions
{
	internal interface IParameterBinder
	{
		object BuildParameter(Type parameterType, string[] args);
	}
}
