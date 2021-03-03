using System.Collections.Generic;
using TinyIoc.Contract;

namespace TinyIoc.Core.ActivatorFactory
{
    internal class ActivatorFactoryEqualityComparer : IEqualityComparer<IActivatorFactory>
    {
        public bool Equals(IActivatorFactory x, IActivatorFactory y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Type == y.Type;
        }

        public int GetHashCode(IActivatorFactory obj)
        => obj.Type.GetHashCode();
    }
}
