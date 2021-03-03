using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoc.Contract;
using TinyIoc.Core.Activators;
using TinyIoc.Core.Registration;
using TinyIoc.Extensions;

namespace TinyIoc.Core.Feature
{
    internal class CollectionRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service)
        {
            if (!CanSupport(service, out var type, out var elementType)) return Array.Empty<IComponentRegistration>();

            var listType = typeof(List<>).MakeGenericType(elementType);

            var isList = type.IsGenericListOrCollectionInterfaceType();

            var registration = new ComponentRegistration(
                new DelegateActivator(elementType, (c) =>
                {
                    var rgs = c.ComponentRegistry
                        .RegisterForType(new TypedService(elementType));

                    var items = rgs.Select(c.ResolveComponent).ToArray();

                    var result = Array.CreateInstance(elementType, items.Length);

                    items.CopyTo(result, 0);

                    return isList ? Activator.CreateInstance(listType, result) : result;

                }), service);

            return new[] { registration };
        }

        private static bool CanSupport(Service service, out Type type, out Type elementType)
        {
            elementType = type = null;

            if (!(service is IServiceWithType serviceWithType)) return false;

            type = serviceWithType.ServiceType;

            if (type.IsGenericEnumerableInterfaceType())
            {
                elementType = type.GetGenericArguments().First();

                return true;
            }

            if (!type.IsArray) return false;

            elementType = type.GetElementType();

            return true;

        }
    }
}
