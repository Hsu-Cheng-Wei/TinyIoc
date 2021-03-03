using System;
using TinyIoc.Contract;
using TinyIoc.Core.Registration.Activators;
using TinyIoc.Utility;

namespace TinyIoc.Core
{
    public class ReflectionActivatorData
    {
        public Type Implement { get; }
        public IConstructorFinder ConstructorFinder { get; }

        public ReflectionActivatorData(Type implement)
        {
            Implement = implement;
            ConstructorFinder = new DefaultConstructorFinder();
        }

        public IInstanceActivator Activator =>
            new ReflectionActivator(Implement, ConstructorFinder);
    }
}
