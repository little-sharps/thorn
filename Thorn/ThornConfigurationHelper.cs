using System;
using System.Reflection;

namespace Thorn
{
    public class ThornConfigurationHelper : IThornConfigurationHelper
    {
        public IThornConfigurationHelper WithAttribute(Type attribute)
        {
            return this;
        }

        public IThornConfigurationHelper InAssembly(Assembly assembly)
        {
            return this;
        }
    }
}