using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;
using TinyIoc.Core.ActivatorFactory;

namespace TinyIoc.Core
{
    internal class Registrations
    {
        public Type Type { get; }

        private readonly Dictionary<Type, IActivatorFactory> _resolves;

        public IContainer Container { get; }

        public Registrations(Type type, IContainer container, LifeCycles cycles)
        {
            Type = type;
            Container = container;
            _resolves = new Dictionary<Type, IActivatorFactory>();
        }

        public IEnumerable<object> CreateInstances()
        => _resolves.Values.Select(f => f.ActivatorObject());

        public void AddOrUpdate(Type implement, LifeCycles cycles)
        {
            _resolves.Remove(implement);
            switch (cycles)
            {
                case LifeCycles.Scope:
                    break;
                case LifeCycles.Singleton:
                    break;
                default:
                    _resolves.Add(implement, new TransientFactory(implement, Container));
                    break;
            }
            
        }

        public override bool Equals(object obj)
        => obj is Registrations registrations && registrations.Type == Type;

        public override int GetHashCode()
        => Type.GetHashCode();
    }
}
