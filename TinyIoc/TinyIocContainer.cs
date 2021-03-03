using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;
using TinyIoc.Core;

namespace TinyIoc
{
    public class TinyIocContainer : IContainer
    {
        private readonly Dictionary<Type, Registrations> _registrations = new Dictionary<Type, Registrations>();

        public bool Contains(Type type)
        => _registrations.ContainsKey(type);

        public RegistrationOption Register<TContract, TImplement>()
        => Register(typeof(TContract), typeof(TImplement));

        public RegistrationOption Register(Type contract, Type implement)
        {
            var registrations = new Registrations(contract, this, LifeCycles.Transient);

            _registrations.Add(implement, registrations);

            return new RegistrationOption(implement, registrations);
        }

        public RegistrationOption Register<TImplement>()
        => Register(typeof(TImplement));

        public RegistrationOption Register(Type implement)
        {
            var registrations = new Registrations(implement, this, LifeCycles.Transient);

            _registrations.Add(implement, registrations);

            return new RegistrationOption(implement, registrations);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type instance)
        {
            if (instance.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable<>)))
                return GenericEnumerableResolve(instance);
            return _registrations[instance].CreateInstances().FirstOrDefault();
        }

        private object GenericEnumerableResolve(Type instanceType)
        {
            var p = instanceType.GetGenericArguments().First();

            dynamic result = Activator.CreateInstance(typeof(List<>).MakeGenericType(p));

            foreach (var i in _registrations[p].CreateInstances())
            {
                result.Add(i);
            }

            return result;
        }
    }
}
