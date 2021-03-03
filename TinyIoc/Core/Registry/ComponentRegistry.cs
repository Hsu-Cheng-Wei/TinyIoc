using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;
using TinyIoc.Extensions;
using TinyIoc.Utility;

namespace TinyIoc.Core.Registry
{
    internal class ComponentRegistry : IComponentRegistry
    {
        private readonly object _synchRoot = new object();

        private readonly Stack<IRegistrationSource> _dynamicRegistration =
            new Stack<IRegistrationSource>();

        private readonly IDictionary<Service, ServiceRegistrationInfo> _serviceInfo =
            new Dictionary<Service, ServiceRegistrationInfo>();

        public void Register(IComponentRegistration registration)
        {
            Register(registration, false);
        }

        public bool TryGetRegistration(Service service, out IComponentRegistration registration)
        {
            var info = GetOrAddRegistrationInfo(service);

            return info.TryGetRegistration(out registration);
        }

        public IEnumerable<IComponentRegistration> RegisterForType(Service service)
        => GetOrAddRegistrationInfo(service).Implementations;

        public void AddRegistrationSource(IRegistrationSource source)
        {
            lock (_synchRoot)
            {
                _dynamicRegistration.Push(source);
            }
        }

        private void Register(IComponentRegistration registration, bool isPreserve)
        {
            lock (_synchRoot)
            {
                AddRegistration(registration, isPreserve);
            }
        }

        private void AddRegistration(IComponentRegistration registration, bool isPreserve)
        {
            foreach (var service in registration.Services)
            {
                var info = GetOrAddRegistrationInfo(service);

                info.AddImplementation(registration, isPreserve);
            }
        }

        private ServiceRegistrationInfo GetOrAddRegistrationInfo(Service service)
        {
            return _serviceInfo.GetOrAdd(service, (s) =>
            {
                var info = ServiceInfoUtility.CreateServiceInfo(s);

                var rgs = _dynamicRegistration.SelectMany(registrationSource =>
                registrationSource.RegistrationsFor(service));

                foreach (var rg in rgs)
                    info.AddImplementation(rg, true);

                return info;
            });
        }
    }
}
