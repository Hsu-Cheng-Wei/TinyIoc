using System;
using System.Collections.Generic;
using TinyIoc.Contract;
using TinyIoc.Core.Resolve;
using TinyIoc.Extensions;
using TinyIoc.Utility;

namespace TinyIoc.Core.Lifetime
{
    public class LifetimeScope : ISharingLifetimeScope, IServiceProvider
    {
        public IDisposer Disposer { get; }

        private readonly Dictionary<Guid, object> _shareInstance = new Dictionary<Guid, object>();

        public Guid Id => SelfScopeId;

        public static Guid SelfScopeId = Guid.NewGuid();

        public IComponentRegistry ComponentRegistry { get; }

        public ISharingLifetimeScope RootLifetimeScope { get; }

        public ISharingLifetimeScope ParentLifetimeScope { get; }

        public LifetimeScope(IComponentRegistry registry) : this()
        {
            _shareInstance[SelfScopeId] = this;

            Disposer = new Disposer(this);

            ComponentRegistry = registry;
        }

        internal LifetimeScope(IComponentRegistry registry, ISharingLifetimeScope parentLifetimeScope)
            : this(registry)
        {

            RootLifetimeScope = parentLifetimeScope.RootLifetimeScope;

            ParentLifetimeScope = parentLifetimeScope;
        }

        internal LifetimeScope()
        {
            ParentLifetimeScope = RootLifetimeScope = this;
        }

        public object ResolveComponent(IComponentRegistration registration)
        {
            var op = new ResolveOperation(registration, this);

            var instance = op.Resolve();

            return instance;
        }

        public void Dispose()
        {
            Disposer.Dispose();
        }

        public ILifetimeScope BeginLifetimeScope()
        => new LifetimeScope(ComponentRegistry, this);

        public object GetOrCreateAndShare(Guid id, Func<object> creator)
        => _shareInstance.GetOrAdd(id, (i) => creator());

        public object GetService(Type serviceType)
        {
            return this.Resolve(serviceType);
        }
    }
}
