using System;
using System.Collections.Generic;

namespace Thorn
{
    public class ThornConfiguration : IThornConfiguration
    {
        public IEnumerable<Type> KnownHandlers
        {
            get { return new List<Type>(); }
        }
    }
}