using System;

namespace Thorn.Conventions
{
	public interface IParameterHelpProvider
	{
		string GetHelp(Type parameterType);
	}
}
