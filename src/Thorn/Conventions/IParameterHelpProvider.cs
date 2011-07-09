using System;

namespace Thorn.Conventions
{
	internal interface IParameterHelpProvider
	{
		string GetHelp(Type parameterType);
	}
}
