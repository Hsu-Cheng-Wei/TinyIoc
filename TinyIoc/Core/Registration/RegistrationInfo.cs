using System;
using TinyIoc.Contract;

namespace TinyIoc.Core.Registration
{
    public class RegistrationInfo
    {
        public Guid Id { get; } = Guid.NewGuid();

        public IComponentRegistration Target { get; set; }

        public bool IsPreserved { get; set; } = false;
    }
}
