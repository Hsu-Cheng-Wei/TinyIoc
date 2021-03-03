using System;
using TinyIoc.Contract;

namespace TinyIoc.Core.Lifetime
{
    public class CurrentLifetimeScope : IComponentLifetime
    {
        public ISharingLifetimeScope FindScope(ISharingLifetimeScope scope)
        =>  scope ?? 
            throw new ArgumentNullException(
                $"TinyIoc: scope {typeof(ISharingLifetimeScope).FullName} can't null.");
    }
}
