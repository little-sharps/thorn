using System;
using System.Collections.Generic;
using System.Reflection;

namespace Thorn
{
	internal class ConfigurationPlan : IConfigurationHelper
	{
		readonly List<ITypeSource> _sources = new List<ITypeSource>();
		readonly List<Type> _specifiedExports = new List<Type>();
		ITypeScanningConvention _typeScanningConvention;
		IMemberScanningConvention _memberScanningConvention;
		ITypeInstantiationStrategy _typeInstantiationStrategy;
		string _defaultNamspace;

		public ConfigurationPlan()
		{
			ScanEntryAssembly();
			UseDefaultConstructorToInstantiateExports();
			UseTypeScanningConvention(new DefaultTypeScanningConvention());
			UseMemberScanningConvention(new DefaultMemberScanningConvention());
		}

		public IEnumerable<ITypeSource> TypeSources { get { return _sources; } }
		public IEnumerable<Type> AdditionalExports { get { return _specifiedExports; } }
		public ITypeScanningConvention TypeScanningConvention { get { return _typeScanningConvention; } }
		public IMemberScanningConvention MemberScanningConvention { get { return _memberScanningConvention; } }
		public ITypeInstantiationStrategy TypeInstantiationStrategy { get { return _typeInstantiationStrategy; } }
		public string DefaultNamespace { get { return _defaultNamspace; } }

		public void ScanEntryAssembly()
		{
			Scan(Assembly.GetEntryAssembly(), null);
		}

		public void Scan(Assembly assembly)
		{
			Scan(assembly, null);
		}

		public void Scan(Assembly assembly, string @namespace)
		{
			Scan(new AssemblyScanTypeSource(assembly, @namespace));
		}

		private void Scan(ITypeSource typeSource)
		{
			if (!_sources.Contains(typeSource))
			{
				_sources.Add(typeSource);
			}
		}

		public void DoNotScan()
		{
			_sources.Clear();
		}

		public void UseTypeScanningConvention(ITypeScanningConvention typeScanningConvention)
		{
			_typeScanningConvention = typeScanningConvention;
		}

		public void UseMemberScanningConvention(IMemberScanningConvention memberScanningConvention)
		{
			_memberScanningConvention = memberScanningConvention;
		}

		public void Export<TExport>()
		{
			Export(typeof(TExport));
		}

		public void Export(Type type)
		{
			_specifiedExports.Add(type);
		}

		public void UseCommonServiceLocatorToInstantiateExports()
		{
			_typeInstantiationStrategy = new CommonServiceLocatorInstantiationStrategy();
		}

		public void UseDefaultConstructorToInstantiateExports()
		{
			_typeInstantiationStrategy = new DefaultConstructorInstantiationStrategy();
		}

		public void SetDefaultNamespace(string @namespace)
		{
			_defaultNamspace = @namespace;
		}
	}
}