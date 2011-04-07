using System;
using System.Reflection;

namespace Thorn
{
    public class Action : IAction
    {
        private readonly Type _type;
        private readonly MethodInfo _method;

        public Action(Type type, MethodInfo method)
        {
            _type = type;
            _method = method;
        }

        public Type Type
        {
            get { return _type; }
        }

        public MethodInfo Method
        {
            get { return _method; }
        }

        public void Invoke(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}