using System;
using System.Linq;
using System.Reflection;
using TinyIoc.Contract;

namespace TinyIoc.Core.ActivatorFactory
{
    internal class TransientFactory : IActivatorFactory
    {
        public LifeCycles LifeCycles => LifeCycles.Transient;
        public Type Type { get; }
        public IContainer Container { get; }

        public TransientFactory(Type type, IContainer container)
        {
            Type = type;
            Container = container;
        }

        public object ActivatorObject()
        {
            var cs = GetConstructor();

            var args = cs.GetParameters()
                .Select(p => Container.Resolve(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(Type, args);
        }

        private ConstructorInfo GetConstructor()
        {
            var csInfos = Type
                .GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);

            return csInfos.FirstOrDefault(CanConstructor) ?? throw new SystemException();
        }

        private bool CanConstructor(ConstructorInfo cs)
        => cs.GetParameters()
            .Select(p => p.ParameterType)
            .FirstOrDefault(t => !Container.Contains(t)) == null;
    }
}
