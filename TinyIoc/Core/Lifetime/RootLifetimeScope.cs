using System;
using TinyIoc.Contract;

namespace TinyIoc.Core.Lifetime
{
    public class RootLifetimeScope : IComponentLifetime
    {
        public ISharingLifetimeScope FindScope(ISharingLifetimeScope scope)
        =>  scope?.RootLifetimeScope ??
            throw new ArgumentNullException(
                $"TinyIoc: scope {typeof(ISharingLifetimeScope).FullName} can't null.");
    }
}
