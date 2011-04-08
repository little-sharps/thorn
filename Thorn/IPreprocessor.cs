using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thorn
{
    public interface IPreprocessor
    {
        bool CanHandle(string[] args);
        void Handle(string[] args);
    }
}
