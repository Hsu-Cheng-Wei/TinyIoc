using System;
using TinyIoc.Core;

namespace TinyIoc
{
    public interface IContainer
    {
        bool Contains(Type type);

        RegistrationOption Register<TContract, TImplement>();

        RegistrationOption Register(Type contract, Type implement);

        RegistrationOption Register<TImplement>();

        RegistrationOption Register(Type implement);

        T Resolve<T>();

        object Resolve(Type instance);
    }
}
