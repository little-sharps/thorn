using System;
using System.Collections.Generic;
using System.Reflection;
using Thorn.Conventions;

namespace Thorn.Config
{
	internal class ConfigurationPlan : IConfigurationHelper
	{
		readonly List<ITypeSource> _sources = new List<ITypeSource>();
		readonly List<Type> _specifiedTypes = new List<Type>();
		readonly List<Export> _additionalExports = new List<Export>();
		ITypeScanningConvention _typeScanningConvention;
		ITypeInstantiationStrategy _typeInstantiationStrategy;
		string _defaultNamspace;

		public ConfigurationPlan()
		{
			ScanEntryAssembly();
			UseDefaultConstructorToInstantiateExports();
			UseTypeScanningConvention(new DefaultTypeScanningConvention());
		}

		public IEnumerable<ITypeSource> TypeSources { get { return _sources; } }
		public IEnumerable<Type> AdditionalTypes { get { return _specifiedTypes; } }
		public ITypeScanningConvention TypeScanningConvention { get { return _typeScanningConvention; } }
		public ITypeInstantiationStrategy TypeInstantiationStrategy { get { return _typeInstantiationStrategy; } }
		public string DefaultNamespace { get { return _defaultNamspace; } }

		public IEnumerable<Export> AdditionalExports
		{
			get {
				return _additionalExports;
			}
		}

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

		public void Export<TExport>()
		{
			Export(typeof(TExport));
		}

		public void Export(Type type)
		{
			_specifiedTypes.Add(type);
		}

		public void Export(Export export)
		{
			_additionalExports.Add(export);
		}

		public void UseDefaultConstructorToInstantiateExports()
		{
			_typeInstantiationStrategy = new DefaultConstructorInstantiationStrategy();
		}

		public void UseCallbackToInstantiateExports(Func<Type, object> callback)
		{
			_typeInstantiationStrategy = new CallbackInstantiationStrategy(callback);
		}

		public void SetDefaultNamespace(string @namespace)
		{
			_defaultNamspace = @namespace;
		}

		public void Validate()
		{
			if (_typeInstantiationStrategy == null)
			{
				throw new ConfigurationException("Instantiation strategy is required, but was set to null.");
			}

			if (_typeScanningConvention == null)
			{
				throw new ConfigurationException("Type scanning convention is required, but was set to null.");
			}
		}
	}
}