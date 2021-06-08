using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BlazorEasyAuth.Models
{
    public class Role
    {
        private static readonly HashSet<Role> Roles = new();

        public static IReadOnlyCollection<Role> AllRoles => Roles;
        
        public int Priority { get; }

        public string Name { get; }

        public Role(int priority = -1, [CallerMemberName] string name = null)
        {
            Priority = priority;
            Name = name;

            if (Roles.Contains(this))
                throw new InvalidOperationException("Duplicate role registered: " + Name);
            
            Roles.Add(this);
        }

        public override string ToString() => Name;

        public static implicit operator string(Role role)
            => role.ToString();

        public static bool operator >(Role thisRole, Role otherRole)
            => otherRole is null || thisRole.Priority > otherRole.Priority;

        public static bool operator <(Role thisRole, Role otherRole)
            => thisRole.Priority < otherRole.Priority;

        public static bool operator <=(Role thisRole, Role otherRole)
            => thisRole.Equals(otherRole) || thisRole.Priority < otherRole.Priority;

        public static bool operator >=(Role thisRole, Role otherRole)
            => thisRole.Equals(otherRole) || thisRole.Priority > otherRole.Priority;

        public static bool operator ==(Role thisRole, Role otherRole)
        {
            if (thisRole == null && otherRole == null)
                return true;

            if (thisRole == null || otherRole == null)
                return false;

            return thisRole.Equals(otherRole);
        }

        public static bool operator !=(Role thisRole, Role otherRole)
            => !(thisRole == otherRole);

        public override bool Equals(object obj)
            => obj is Role otherRole && Equals(otherRole);

        private bool Equals(Role other)
        {
            if (other is null)
                return false;

            return Name == other.Name;
        }

        public override int GetHashCode()
            => Name != null ? Name.GetHashCode() : 0;
    }
}