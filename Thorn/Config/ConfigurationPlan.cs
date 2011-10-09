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
		private bool _scanEntryAssembly;
		Type _defaultType;
		string _switchDelimiter;

		public ConfigurationPlan()
		{
			ScanEntryAssembly();
			UseDefaultConstructorToInstantiateExports();
			UseTypeScanningConvention(new DefaultTypeScanningConvention());
			UseDefaultSwitchDelimiter();
		}

		public IEnumerable<ITypeSource> TypeSources
		{
			get
			{
				if (_scanEntryAssembly)
				{
					yield return new AssemblyScanTypeSource(Assembly.GetEntryAssembly(), null);
				}
				foreach (var typeSource in _sources)
				{
					yield return typeSource;
				}
			}
		}

		public IEnumerable<Type> AdditionalTypes { get { return _specifiedTypes; } }
		public IEnumerable<Export> AdditionalExports { get { return _additionalExports; } }
		public ITypeScanningConvention TypeScanningConvention { get { return _typeScanningConvention; } }
		public ITypeInstantiationStrategy TypeInstantiationStrategy { get { return _typeInstantiationStrategy; } }
		public Type DefaultType { get { return _defaultType; } }
		public string SwitchDelimiter { get { return _switchDelimiter; } }

		public void ScanEntryAssembly()
		{
			_scanEntryAssembly = true;
		}

		public void DoNotScanEntryAssembly()
		{
			_scanEntryAssembly = false;
		}

		[Obsolete("Use method DoNotScanEntryAssembly() instead", false)]
		public void DoNotScan()
		{
			Console.WriteLine("DEPRECATED: In Thorn configuration, change DoNotScan() to DoNotScanEntryAssembly()");
			DoNotScanEntryAssembly();
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

		public void SetDefaultType<T>()
		{
			SetDefaultType(typeof(T));
		}

		public void SetDefaultType(Type type)
		{
			_defaultType = type;
		}

		public void UseDefaultSwitchDelimiter()
		{
			_switchDelimiter = null;
		}

		public void UseDashForSwitchDelimiter()
		{
			_switchDelimiter = "-";
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