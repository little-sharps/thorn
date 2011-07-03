using System;
using System.Collections.Generic;
using System.Linq;

namespace Thorn
{
	public class DefaultTypeScanningConvention : ITypeScanningConvention
	{
		public IEnumerable<Export> GetExports(IQueryable<Type> typesToScan, IMemberScanningConvention memberScanningConvention)
		{
			return  from type in typesToScan
					where type.GetCustomAttributes(false).Any(attr => attr is ThornExportAttribute)
					let attr = type.GetCustomAttributes(false).First(a => a is ThornExportAttribute) as ThornExportAttribute
					select new Export(type, attr.IsDefault, GetName(type), memberScanningConvention.GetActions(type));
		}

		private string GetName(Type type)
		{
			return type.Name.ToLower();
		}
	}
}