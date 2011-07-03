using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Thorn
{
	public class Configuration
	{
		private Dictionary<string, Export> _exports = new Dictionary<string, Export>();
		private ITypeInstantiationStrategy _instantiationStrategy;
		private string _defaultNamespace;

		public ITypeInstantiationStrategy InstantiationStrategy
		{
			get { return _instantiationStrategy; }
			set { _instantiationStrategy = value; }
		}

		public IEnumerable<Export> Exports
		{
			get { return _exports.Values; }
		}

		public Export GetExportByNamespace(string @namespace)
		{
			return _exports[@namespace];
		}
		
		public void AddExport(Export export)
		{
			_exports[export.Namespace] = export;
		}

		public string DefaultNamespace
		{
			get
			{
				if (_defaultNamespace == null)
				{
					_defaultNamespace = DetermineDefaultNamespace();
				}
				return _defaultNamespace;
			}
			set { _defaultNamespace = value; }
		}

		private string DetermineDefaultNamespace()
		{
			string result = null;
			foreach (var ns in _exports.Keys)
			{
				var export = _exports[ns];
				if (result == null || export.IsDefault)
				{
					result = ns;
				}
			}
			return result;
		}
		
		public void AssertConfigurationIsValid()
		{
			foreach (var export in _exports.Values)
			{
				ValidateExport(export);
			}
		}

		public void ValidateExport(Export export)
		{
			foreach (var action in export.Actions)
			{
				ValidateAction(action);
			}
		}

		private void ValidateAction(Action action)
		{
			if (action.MethodParameters.Length > 1)
			{
				throw new ConfigurationException(String.Format("The method {0}.{1} cannot be exported, as it has too many arguments. Perhaps you should [ThornIgnore] it.", action.Type.FullName, action.Method.Name));
			}
			if (action.MethodParameters.Length == 1)
			{
				var type = action.MethodParameters[0].ParameterType;
				if (!type.IsClass)
				{
					throw new ConfigurationException(String.Format("The method {0}.{1} cannot be exported, it's argument type is not a class.", action.Type.FullName, action.Method.Name));
				}
				var constructors = type.GetConstructors(BindingFlags.Public);
				if (constructors.Length > 0 && !constructors.Any(ci => ci.GetParameters().Length == 0))
				{
					throw new ConfigurationException(String.Format("The method {0}.{1} cannot be exported, it's argument type does not have a default public constructor.", action.Type.FullName, action.Method.Name));
				}
			}
		}
	}
}