using System;
using System.Reflection;

namespace Thorn
{
    public interface IThornConfigurationHelper
    {
        IThornConfigurationHelper WithAttribute(Type attribute);
        IThornConfigurationHelper InAssembly(Assembly assembly);
    }
}