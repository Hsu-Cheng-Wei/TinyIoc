using System;
using System.Collections.Generic;
using TinyIoc.Core;

namespace TinyIoc.Contract
{
    public interface IRegistrationSource
    {
        IEnumerable<IComponentRegistration> RegistrationsFor(Service service);
    }
}
