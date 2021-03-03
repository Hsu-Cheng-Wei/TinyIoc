using System;
using TinyIoc.Contract;
using TinyIoc.Core.Lifetime;

namespace TinyIoc.Core.Resolve
{
    internal class ResolveOperation
    {
        private readonly IComponentRegistration _component;
        private readonly ISharingLifetimeScope _scope;

        public ResolveOperation(IComponentRegistration component,ISharingLifetimeScope scope)
        {
            _component = component;
            _scope = component.Lifetime.FindScope(scope);
        }

        public object Resolve()
        {
            var instance =  _component.Sharing == InstanceSharing.None ?
                Activiator() :
                _scope.GetOrCreateAndShare(_component.Id, Activiator);

            if (instance is IDisposable disposable)
                _scope.Disposer.AddInstanceForDisposal(disposable);

            return instance;
        }

        private object Activiator()
        => _component.Activator.ActivateInstance(_scope);
    }
}
