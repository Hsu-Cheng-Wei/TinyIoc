using System;
using TinyIoc.Contract;
using TinyIoc.Core;
using TinyIoc.Core.Registration;

namespace TinyIoc.Builder
{
    internal class RegistrationBuilder<TLimit> : IRegistrationBuilder<TLimit>
    {
        public Type DefaultType { get; } = typeof(TLimit);

        public IInstanceActivator Activator { get; set; }

        public RegistrationData RegistrationData { get; }

        public RegistrationInfo RegistrationInfo { get; }

        public RegistrationBuilder(Service defaultService)
        {
            RegistrationData = new RegistrationData(defaultService);
            Activator = new ReflectionActivatorData(typeof(TLimit)).Activator;
            RegistrationInfo = new RegistrationInfo();
        }

        public IRegistrationBuilder<TLimit> As(Type service)
        {
            if (!service.IsAssignableFrom(DefaultType))
                throw new InvalidOperationException(
                    $"TinyIoc: {service.FullName} is not assignable from {DefaultType.FullName}");

            RegistrationData.AddServices(new TypedService(service));

            return this;
        }

        public IRegistrationBuilder<TLimit> As<TService>()
        => As(typeof(TService));
    }
}
