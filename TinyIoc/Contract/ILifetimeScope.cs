using System;

namespace TinyIoc.Contract
{
    public interface ILifetimeScope : IComponentContext, IDisposable
    {
        Guid Id { get; }

        ILifetimeScope BeginLifetimeScope();
    }
}
