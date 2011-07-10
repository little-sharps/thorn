using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Thorn.Conventions
{
	public interface ITypeScanningConvention
	{
		IQueryable<Type> TypesToExport(IQueryable<Type> typesToScan);
		IEnumerable<MethodInfo> MethodsToExport(Type type);

		string GetNamespace(Type type);
		string GetName(Type type, MethodInfo methodInfo);
		string GetDescription(Type type, MethodInfo methodInfo);
	}
}