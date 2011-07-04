using System;
using System.Collections.Generic;
using System.Reflection;
using Thorn.Config;
using Thorn.Conventions;

namespace Thorn
{
	public class TypeScanner
	{
		private readonly ITypeScanningConvention _convention;

		public TypeScanner(ITypeScanningConvention convention)
		{
			_convention = convention;
		}

		public IEnumerable<Export> GetExportsIn(ITypeSource source)
		{
			var result = new List<Export>();
			foreach (var type in _convention.TypesToExport(source.Types))
			{
				result.AddRange(GetExportsIn(type));
			}
			return result;
		}

		public IEnumerable<Export> GetExportsIn(Type type)
		{
			var result = new List<Export>();
			foreach (var method in _convention.MethodsToExport(type))
			{
				result.Add(BuildExport(type, method));
			}
			return result;
		}

		private Export BuildExport(Type type, MethodInfo method)
		{
			return new Export(  
								type, 
								method, 
								_convention.GetNamespace(type, method), 
								_convention.GetName(type, method), 
								_convention.GetDescription(type, method)
							 );
		}
	}
}
