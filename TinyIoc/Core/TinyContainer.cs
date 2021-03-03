using System;
using TinyIoc.Contract;
using TinyIoc.Core.Activators;
using TinyIoc.Core.Lifetime;
using TinyIoc.Core.Registration;
using TinyIoc.Core.Registry;

namespace TinyIoc.Core
{
    public class TinyContainer : IContainer
    {
        public IComponentRegistry ComponentRegistry { get; }

        public ILifetimeScope RootLifetimeScope { get; }

        public Guid Id { get; } = Guid.NewGuid();

        internal TinyContainer()
        {
            ComponentRegistry = new ComponentRegistry();

            ComponentRegistry.Register(new ComponentRegistration(
                LifetimeScope.SelfScopeId,
                new DelegateActivator(typeof(LifetimeScope), c => throw new InvalidOperationException()),
                new CurrentLifetimeScope(),
                InstanceSharing.Shared,
                new TypedService(typeof(ILifetimeScope)),
                new TypedService(typeof(IComponentContext)
                )));

            RootLifetimeScope = new LifetimeScope(ComponentRegistry);
        }

        public ILifetimeScope BeginLifetimeScope()
        => RootLifetimeScope.BeginLifetimeScope();

        public object ResolveComponent(IComponentRegistration registration)
        => RootLifetimeScope.ResolveComponent(registration);

        public void Dispose()
        {
            RootLifetimeScope.Dispose();
        }
    }
}
