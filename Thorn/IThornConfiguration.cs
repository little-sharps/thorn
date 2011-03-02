using System;
using System.Collections.Generic;

namespace Thorn
{
    public interface IThornConfiguration
    {
        IEnumerable<Type> KnownHandlers { get; }
    }
}