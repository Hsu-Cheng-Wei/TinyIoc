using System;
using TinyIoc.Contract;

namespace TinyIoc.Core.Registration.Activators
{
    public abstract class InstanceActivator : IInstanceActivator
    {
        public Type LimitType { get; }

        protected InstanceActivator(Type limitType)
        {
            LimitType = limitType;
        }

        public abstract object ActivateInstance(IComponentContext context);
    }
}
