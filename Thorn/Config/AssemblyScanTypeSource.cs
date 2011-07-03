using System;
using System.Linq;
using System.Reflection;

namespace Thorn.Config
{
	internal class AssemblyScanTypeSource : ITypeSource
	{
		public AssemblyScanTypeSource(Assembly assembly, string @namespace)
		{
			Assembly = assembly;
			Namespace = @namespace;
		}

		public Assembly Assembly { get; private set; }
		public string Namespace { get; private set; }

		public IQueryable<Type> Types
		{
			get
			{
				var query = Assembly.GetTypes().AsQueryable();
				if (!string.IsNullOrEmpty(Namespace))
				{
					query = query.Where(t => t.Namespace == Namespace);
				}
				return query;
			}
		}
	}
}