using System;
using TinyIoc.Contract;

namespace TinyIoc.Core
{
    public class TypedService : Service, IServiceWithType
    {
        public Type ServiceType { get; }

        public override string Description => ServiceType.FullName;

        public TypedService(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TypedService service)) return false;

            return ServiceType == service.ServiceType;
        }

        public override int GetHashCode()
        => ServiceType.GetHashCode();

        public Service ChangeType(Type newType)
        => new TypedService(newType);
    }
}
