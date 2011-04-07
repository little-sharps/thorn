using System;
using System.Reflection;

namespace Thorn
{
    public interface IAction
    {
        Type Type { get; }
        MethodInfo Method { get; }
        void Invoke(string[] args);
    }
}