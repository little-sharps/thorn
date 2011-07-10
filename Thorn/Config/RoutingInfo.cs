using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thorn.Config
{
	internal class RoutingInfo
	{
		private string _defaultNamespace;
		private readonly IDictionary<string, IList<Export>> _exports = new Dictionary<string, IList<Export>>();

		public RoutingInfo(IEnumerable<Export> exports, string defaultNamespace = null)
		{
			_defaultNamespace = defaultNamespace;
			foreach (var export in exports)
			{
				AddExport(export);
			}
		}

		public string DefaultNamespace
		{
			get { return _defaultNamespace; }
		}

		public IEnumerable<Export> Exports
		{
			get
			{
				return from list in _exports.Values
				       from export in list
				       select export;
			}
		}

		public IEnumerable<Export> ExportsInNamespace(string ns)
		{
			return _exports[ns];
		}

		private void AddExport(Export export)
		{
			if (!_exports.ContainsKey(export.Namespace))
			{
				_exports[export.Namespace] = new List<Export>();
			}
			_exports[export.Namespace].Add(export);
		}

		public void Validate()
		{
			if (_exports.Values.Sum(list => list.Count) == 0)
				throw new ConfigurationException("Nothing has been exported. Did you mark your class with a [ThornExport] attribute?");

			foreach (var export in Exports)
			{
				export.Validate();
			}

			CheckForNameCollisions();
		}

		private void CheckForNameCollisions()
		{
			foreach (var key in _exports.Keys)
			{
				foreach (var name in _exports[key].Select(export => export.Name).Distinct())
				{
					if (_exports[key].Count(export => export.Name == name) > 1)
					{
						var sb = new StringBuilder();
						sb.AppendLine("Exported names must be unique within a namespace. The following conflict:");

						foreach (var export in _exports[key].Where(export => export.Name == name))
						{
							sb.AppendFormat("{0}\n", export);
						}

						throw new ConfigurationException(sb.ToString());
					}
				}
			}
		}
	}
}
