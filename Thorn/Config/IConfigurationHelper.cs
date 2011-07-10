using System;
using System.Reflection;
using Thorn.Conventions;

namespace Thorn.Config
{
	public interface IConfigurationHelper
	{
		void ScanEntryAssembly();
		void Scan(Assembly assembly);
		void Scan(Assembly assembly, string @namespace);
		void DoNotScan();

		void UseTypeScanningConvention(ITypeScanningConvention typeScanningConvention);

		void Export<TExport>();
		void Export(Type type);
		void Export(Export export);

		void UseDefaultConstructorToInstantiateExports();
		void UseCallbackToInstantiateExports(Func<Type, object> callback);

		void SetDefaultType<T>();
		void SetDefaultType(Type type);

		void UseDefaultSwitchDelimiter();
		void UseDashForSwitchDelimiter();
	}
}