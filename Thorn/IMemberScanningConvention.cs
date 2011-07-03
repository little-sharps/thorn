using System;
using System.Collections.Generic;
using System.Reflection;

namespace Thorn
{
	public interface IMemberScanningConvention
	{
		IEnumerable<Action> GetActions(Type type);
	}
}