using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Thorn
{
    public class AssemblyScanner
    {
        private IQueryable<Type> DecoratedTypesIn(Assembly assembly)
        {
            return assembly.GetTypes().AsQueryable()
                .Where(
                    t => t.GetCustomAttributes(false).AsQueryable().Any(attr => attr is ThornExportAttribute)
                );
        }

        public Type[] GetDecoratedTypesIn(Assembly assembly)
        {
            return DecoratedTypesIn(assembly).ToArray();
        }

        public Type[] GetDecoratedTypesIn(Assembly assembly, string @namespace)
        {
            return DecoratedTypesIn(assembly).Where(t => t.Namespace == @namespace).ToArray();
        }

        public MethodInfo[] GetRoutableMethodsOn(Type type)
        {
            return type.GetMethods().AsQueryable()
                .Where(m => m.DeclaringType == type)
                .Where(
                    m => !(m.GetCustomAttributes(false).AsQueryable().Any(attr => attr is ThornIgnoreAttribute))
                )
                .ToArray();
        }

        public IEnumerable<IAction> GetActionsFrom(Assembly assembly)
        {
            var result = new List<IAction>();

            foreach (var type in GetDecoratedTypesIn(assembly))
            {
                foreach (var methodInfo in GetRoutableMethodsOn(type))
                {
                    result.Add(BuildAction(type, methodInfo));
                }
            }

            return result;
        }

        public IEnumerable<IAction> GetActionsFrom(Assembly assembly, string @namespace)
        {
            var result = new List<IAction>();

            foreach (var type in GetDecoratedTypesIn(assembly, @namespace))
            {
                foreach (var methodInfo in GetRoutableMethodsOn(type))
                {
                    result.Add(BuildAction(type, methodInfo));
                }
            }

            return result;
        }

        private IAction BuildAction(Type type, MethodInfo methodInfo)
        {
            return new Action(type, methodInfo);
        }





    }
}