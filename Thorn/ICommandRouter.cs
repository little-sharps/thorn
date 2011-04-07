using System.Collections.Generic;

namespace Thorn
{
    public interface ICommandRouter
    {
        IAction FindAction(string commandName);
    }
}