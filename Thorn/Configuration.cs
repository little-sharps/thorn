using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Thorn
{
    public class Configuration
    {
        private static bool _hasBeenConfigured;
        private static List<Action> _knownActions = new List<Action>();
        private static Type _defaultReceiver;
        private static AssemblyScanner _scanner = new AssemblyScanner();
        
        public static void IncludeEntryAssembly()
        {
            RegisterActions(_scanner.GetActionsFrom(Assembly.GetEntryAssembly()));
        }

        public static void IncludeFrom(Assembly assembly)
        {
            RegisterActions(_scanner.GetActionsFrom(assembly));
        }

        public static void IncludeFrom(Assembly assembly, string @namespace)
        {
            RegisterActions(_scanner.GetActionsFrom(assembly, @namespace));
        }

        internal static IEnumerable<Action> GetConfiguredActions()
        {
            if (!_hasBeenConfigured)
            {
                IncludeEntryAssembly();
            }
            return _knownActions;
        }

        private static void RegisterActions(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                RegisterAction(action);
            }
        }

        private static void RegisterAction(Action action)
        {
            ValidateAction(action);
            _knownActions.Add(action);
            _hasBeenConfigured |= true;
        }

        private static void ValidateAction(Action action)
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

            foreach (var knownAction in _knownActions)
            {
                if (knownAction == action)
                {
                    throw new ConfigurationException(String.Format("Attempt to register {0}.{1} twice.", action.Type.FullName, action.Method.Name));
                }
                if (knownAction.GetRoutingInfo() == action.GetRoutingInfo())
                {
                    throw new ConfigurationException("Two exports apear to conflict. Thorn does not support exporting overloaded methods. Also, Thorn is a case insensitive tool, so method names must differ in more than casing.");
                }
            }
        }


        public static Type GetDefaultReceiver()
        {
            if (_defaultReceiver == null)
            {
                var knownTypes = _knownActions.Select(a => a.Type).Distinct().ToArray();
                if (knownTypes.Length == 1)
                {
                    _defaultReceiver = knownTypes[0];
                }
            }

            return _defaultReceiver;
        }
    }
}