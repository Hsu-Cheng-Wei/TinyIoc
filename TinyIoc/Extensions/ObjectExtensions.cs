using System;

namespace TinyIoc.Extensions
{
    internal static class ObjectExtensions
    {
        public static object GetDefaultValue(this Type type)
        => type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}
