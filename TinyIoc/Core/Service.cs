using System;

namespace TinyIoc.Core
{
    public abstract class Service
    {
        public abstract string Description { get; }

        public override string ToString()
        {
            return Description;
        }

        public static bool operator ==(Service left, Service right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Service left, Service right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }


    }
}
