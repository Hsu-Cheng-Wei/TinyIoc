using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoc.Contract;

namespace TinyIoc.Core
{
    public class RegistrationOption
    {
        private readonly Type _implement;
        private readonly Registrations _registrations;

        internal RegistrationOption(Type implement, Registrations registrations)
        {
            _implement = implement;
            _registrations = registrations;
        }

        public IContainer AsSingleton()
        {
            _registrations.AddOrUpdate(_implement, LifeCycles.Singleton);

            return _registrations.Container;
        }

        public IContainer AsTransient()
        {
            _registrations.AddOrUpdate(_implement, LifeCycles.Transient);

            return _registrations.Container;
        }

        public IContainer AsScope()
        {
            _registrations.AddOrUpdate(_implement, LifeCycles.Scope);

            return _registrations.Container;
        }
    }
}
