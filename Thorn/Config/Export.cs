using System;
using System.Linq;
using System.Reflection;
using Thorn.Exceptions;

namespace Thorn.Config
{
	public class Export
	{
		private readonly Type _type;
		private readonly MethodInfo _method;
		private readonly string _name;
		private readonly string _description;
		private string _ns;

		public Export(Type type, MethodInfo method, string ns, string name, string description = null)
		{
			_type = type;
			_method = method;
			_ns = ns;
			_name = name;
			_description = description;
		}

		public string Namespace
		{
			get { return _ns; }
		}

		public string Name
		{
			get { return _name; }
		}

		public string Description
		{
			get { return _description; }
		}

		public Type Type
		{
			get { return _type; }
		}

		public MethodInfo Method
		{
			get { return _method; }
		}

		public Type ParameterType
		{
			get { return _method.GetParameters()[0].ParameterType; }
		}

		public void Validate()
		{
			var methodParameters = _method.GetParameters();
			if (methodParameters.Length > 1)
			{
				throw new ConfigurationException(String.Format("The method {0}.{1} cannot be exported, as it has too many arguments. Perhaps you should [ThornIgnore] it.", Type.FullName, Method.Name));
			}
			if (methodParameters.Length == 1)
			{
				var type = methodParameters[0].ParameterType;
				if (!type.IsClass)
				{
					throw new ConfigurationException(String.Format("The method {0}.{1} cannot be exported, it's argument type is not a class.", Type.FullName, Method.Name));
				}
				var constructors = type.GetConstructors(BindingFlags.Public);
				if (constructors.Length > 0 && !constructors.Any(ci => ci.GetParameters().Length == 0))
				{
					throw new ConfigurationException(String.Format("The method {0}.{1} cannot be exported, it's argument type does not have a default public constructor.", Type.FullName, Method.Name));
				}
			}
		}

		public override string ToString()
		{
			return string.Format("{0}:{1} ({2}.{3})", Namespace, Name, Type.Name, Method.Name);
		}
	}
}