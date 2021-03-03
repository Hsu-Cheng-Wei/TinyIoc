using System;
using TinyIoc.Contract;
using TinyIoc.Core.Registration.Activators;

namespace TinyIoc.Core.Activators
{
    public class DelegateActivator : InstanceActivator, IInstanceActivator 
    {
        private readonly Func<IComponentContext, object> _activationFunction;

        public DelegateActivator(Type limitType, Func<IComponentContext, object> activationFunction) : base(limitType)
        {
            _activationFunction = activationFunction;
        }

        public override object ActivateInstance(IComponentContext context)
        {
            return _activationFunction(context);
        }
    }
}
