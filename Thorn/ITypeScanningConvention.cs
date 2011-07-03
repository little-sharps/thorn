using System;
using System.Collections.Generic;
using System.Linq;

namespace Thorn
{
	public interface ITypeScanningConvention
	{
		IEnumerable<Export> GetExports(IQueryable<Type> typesToScan, IMemberScanningConvention memberScanningConvention);
	}
}