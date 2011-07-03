using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Thorn
{
	public class Action : IEquatable<Action>
	{
		private readonly Type _type;
		private readonly MethodInfo _method;
		private readonly string _name;

		public Action(Type type, MethodInfo method, string name)
		{
			_type = type;
			_method = method;
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}

		public Type Type
		{
			get { return _type; }
		}

		public MethodInfo Method
		{
			get { return _method; }
		}

		internal ParameterInfo[] MethodParameters
		{
			get { return _method.GetParameters(); }
		}

		public void Invoke(object instance, string[] args)
		{
			var parameters = new List<object>();

			if (MethodParameters.Length > 1)
			{
				throw new InvocationException("Unable to invoke method " + _type.FullName + "." + _method.Name + ". It has too many parameters. Please see the Thorn Readme on Bindable Parameters");
			}

			if (MethodParameters.Length == 1)
			{
				parameters.Add(BuildInvocationParameter(MethodParameters[0].ParameterType, args));
			}

			_method.Invoke(instance, parameters.ToArray());
		}

		private object BuildInvocationParameter(Type type, string[] args)
		{
			return GetArgsModelBindingDefinitionForType(type).CreateAndBind(args);
		}

		public string GetDescription()
		{
			DescriptionAttribute descriptionAttr = null;
			foreach (var attribute in _method.GetCustomAttributes(typeof(DescriptionAttribute), false))
			{
				descriptionAttr = attribute as DescriptionAttribute;
			}

			return descriptionAttr == null ? String.Empty : descriptionAttr.Description;
		}

		public string GetHelp()
		{
			if (MethodParameters.Length == 1)
			{
				return GetArgsHelp();
			}
			return String.Empty;
		}

		private string GetArgsHelp()
		{
			var modelhelp =
				new Args.Help.HelpProvider().GenerateModelHelp(
					GetArgsModelBindingDefinitionForType(MethodParameters[0].ParameterType));

			var helpFormatter = new Args.Help.Formatters.ConsoleHelpFormatter(80, 1, 5);

			var sb = new StringBuilder();
			using (var writer = new StringWriter(sb))
			{
				helpFormatter.WriteHelp(modelhelp, writer);
				writer.Close();
			}
			return sb.ToString();
		}

		private dynamic GetArgsModelBindingDefinitionForType(Type type)
		{
			var genericConfigureMethod = typeof(Args.Configuration).GetMethods(BindingFlags.Static | BindingFlags.Public).AsQueryable()
				.First(m => m.Name == "Configure" && m.GetParameters().Count() == 0);

			var closedConfigureMethod = genericConfigureMethod.MakeGenericMethod(type);

			return closedConfigureMethod.Invoke(null, new object[0]);
		}

		#region Actions are equatable
		
		public bool Equals(Action other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._type, _type) && Equals(other._method, _method);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Action)) return false;
			return Equals((Action) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_type.GetHashCode()*397) ^ _method.GetHashCode();
			}
		}

		public static bool operator ==(Action left, Action right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Action left, Action right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}