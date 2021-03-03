using System.Collections.Generic;
using TinyIoc.Core;

namespace TinyIoc.Contract
{
    public interface IComponentRegistry
    {
        void Register(IComponentRegistration registration);

        bool TryGetRegistration(Service service, out IComponentRegistration registration);

        IEnumerable<IComponentRegistration> RegisterForType(Service service);

        void AddRegistrationSource(IRegistrationSource source);
    }
}
