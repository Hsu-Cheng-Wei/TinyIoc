using System;
using TinyIoc.Contract;
using TinyIoc.Core;
using TinyIoc.Core.Activators;
using TinyIoc.Core.Lifetime;
using TinyIoc.Core.Registration;
using TinyIoc.Extensions;

namespace TinyIoc.Builder
{
    public class RegistrationBuilderFactory
    {
        public static IRegistrationBuilder<object> ForType(Type limitType)
        {
            var builderType = typeof(RegistrationBuilder<>).MakeGenericType(limitType);

            return (IRegistrationBuilder<object>)Activator
                .CreateInstance(builderType, new TypedService(limitType));
        }

        public static IRegistrationBuilder<TLimit> ForType<TLimit>()
        => (IRegistrationBuilder<TLimit>)ForType(typeof(TLimit));

        public static IRegistrationBuilder<TInstance> ForInstance<TInstance>(TInstance instance)
        {
            var type = typeof(TInstance);

            var builder = new RegistrationBuilder<TInstance>(
                new TypedService(type));

            builder.Activator = new DelegateActivator(type, c => instance);

            builder.SingleInstance();

            return builder;
        }

        public static void RegisterSingleComponent<TLimit>(
            IComponentRegistry cr,
            IRegistrationBuilder<TLimit> builder)
        {
            cr.Register(new ComponentRegistration(
                builder.RegistrationInfo.Id,
                builder.Activator,
                builder.RegistrationData.Lifetime,
                builder.RegistrationData.Sharing,
                builder.RegistrationData.Services
                ));
        }
    }
}
