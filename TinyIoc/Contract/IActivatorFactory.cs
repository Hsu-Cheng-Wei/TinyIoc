using System;

namespace TinyIoc.Contract
{
    internal interface IActivatorFactory
    {
        LifeCycles LifeCycles { get; }

        Type Type { get; }

        IContainer Container { get; }

        object ActivatorObject();
    }
}
