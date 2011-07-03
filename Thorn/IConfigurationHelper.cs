using System;
using System.Reflection;

namespace Thorn
{
	public interface IConfigurationHelper
	{
		void ScanEntryAssembly();
		void Scan(Assembly assembly);
		void Scan(Assembly assembly, string @namespace);
		void DoNotScan();

		void UseTypeScanningConvention(ITypeScanningConvention typeScanningConvention);
		void UseMemberScanningConvention(IMemberScanningConvention memberScanningConvention);

		void Export<TExport>();
		void Export(Type type);

		void UseCommonServiceLocatorToInstantiateExports();
		void UseDefaultConstructorToInstantiateExports();

		void SetDefaultNamespace(string @namespace);
	}
}