using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;

namespace TinyIoc.Core
{
    internal class ServiceRegistrationInfo
    {
        private readonly LinkedList<IComponentRegistration> _implementations =
            new LinkedList<IComponentRegistration>();

        public Service Service { get; }

        public IComponentRegistration[] Implementations => _implementations.ToArray();

        public ServiceRegistrationInfo(Service service)
        {
            Service = service;
        }

        public void AddImplementation(IComponentRegistration registration, bool isOverride)
        {
            if (isOverride)
                _implementations.AddFirst(registration);
            else
                _implementations.AddLast(registration);
        }

        public bool TryGetRegistration(out IComponentRegistration registration)
        {
            registration = null;

            if (!_implementations.Any()) return false;

            registration = _implementations.First.Value;

            return true;
        }

    }
}
