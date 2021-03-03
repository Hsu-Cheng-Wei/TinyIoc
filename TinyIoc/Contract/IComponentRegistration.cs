using System;
using TinyIoc.Core;
using TinyIoc.Core.Lifetime;

namespace TinyIoc.Contract
{
    public interface IComponentRegistration
    {
        Guid Id { get; }

        IInstanceActivator Activator { get; }

        IComponentLifetime Lifetime { get; }

        InstanceSharing Sharing { get; }

        Service[] Services { get; }
    }
}
