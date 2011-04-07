using System;

namespace Thorn
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ThornIgnoreAttribute : Attribute
    {

    }
}