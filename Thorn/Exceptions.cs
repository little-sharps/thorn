using System;

namespace Thorn
{
    public class RoutingException : Exception
    {
        private string _command;

        public RoutingException(string command)
        {
            _command = command;
        }

        public string Command
        {
            get { return _command; }
        }
    }

    public class ConfigurationException : Exception 
    {
        public ConfigurationException(string s) : base(s) {}
    }

    public class InvocationException : Exception
    {
        public InvocationException(string s) : base(s) { }
    }
}