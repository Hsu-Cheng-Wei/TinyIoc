using System;
using TinyIoc.Contract;
using TinyIoc.Core;

namespace TinyIoc.Extensions
{
    public static class ResolveExtensions
    {
        public static T Resolve<T>(this IComponentContext componentContext)
        {
            if(!componentContext.TryResolve<T>(out var instance))
                throw new InvalidOperationException($"TinyIoc: Not register {typeof(T).FullName}");

            return instance;
        }

        public static object Resolve(this IComponentContext componentContext, Type instanceType)
        {
            if (!componentContext.TryResolve(instanceType, out var instance))
                throw new InvalidOperationException($"TinyIoc: Not register {instanceType.FullName}");

            return instance;
        }

        public static bool TryResolve<T>(this IComponentContext componentContext, out T instance)
        {
            var result = componentContext.TryResolve(typeof(T), out var instanceObj);

            instance = (T) instanceObj;

            return result;
        }

        public static bool TryResolve(this IComponentContext componentContext, Type instanceType, out object instance)
        {
            instance = instanceType.GetDefaultValue();

            var type = new TypedService(instanceType);

            if (!componentContext.ComponentRegistry.TryGetRegistration(type, out var re))
                return false;

            instance = componentContext.ResolveComponent(re);

            return true;
        }
    }
}
