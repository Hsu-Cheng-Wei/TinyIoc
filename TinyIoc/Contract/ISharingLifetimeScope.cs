using System;

namespace TinyIoc.Contract
{
    public interface ISharingLifetimeScope : ILifetimeScope
    {
        IDisposer Disposer { get; }

        ISharingLifetimeScope RootLifetimeScope { get; }

        ISharingLifetimeScope ParentLifetimeScope { get; }

        object GetOrCreateAndShare(Guid id, Func<object> creator);
    }
}
