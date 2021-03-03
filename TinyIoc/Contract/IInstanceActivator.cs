using System;

namespace TinyIoc.Contract
{
    public interface IInstanceActivator
    {
        object ActivateInstance(IComponentContext context);

        Type LimitType { get; }
    }
}
