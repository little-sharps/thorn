using System;

namespace Thorn
{
    public class RoutingInfo : IEquatable<RoutingInfo>
    {
        private readonly string _scope;
        private readonly string _command;

        public RoutingInfo(string scope, string command)
        {
            _scope = scope.ToLower();
            _command = command.ToLower();
        }
        
        public string Scope
        {
            get { return _scope; }
        }

        public string Command
        {
            get { return _command; }
        }

        #region RoutingInfos are equatable
        
        public bool Equals(RoutingInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._scope, _scope) && Equals(other._command, _command);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (RoutingInfo)) return false;
            return Equals((RoutingInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_scope.GetHashCode()*397) ^ _command.GetHashCode();
            }
        }

        public static bool operator ==(RoutingInfo left, RoutingInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RoutingInfo left, RoutingInfo right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}