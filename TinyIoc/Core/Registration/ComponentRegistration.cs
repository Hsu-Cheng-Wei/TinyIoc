using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;
using TinyIoc.Core.Lifetime;

namespace TinyIoc.Core.Registration
{
    public class ComponentRegistration : IComponentRegistration
    {
        public Guid Id { get; }

        public IInstanceActivator Activator { get; }

        public IComponentLifetime Lifetime { get; }

        public InstanceSharing Sharing { get; }

        public Service[] Services { get; }

        public ComponentRegistration(IInstanceActivator activator, params Service[] services)
            :this(Guid.NewGuid(), activator, new CurrentLifetimeScope(), InstanceSharing.None, services) { }

        public ComponentRegistration(
            Guid id,
            IInstanceActivator activator,
            IComponentLifetime lifetime,
            InstanceSharing sharing,
            params Service[] services)
        {
            Id = id;
            
            Activator = activator;

            Lifetime = lifetime;

            Sharing = sharing;

            Services = services.ToArray();
        }
    }
}
