using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyIoc.Extensions
{
    public static class TypeExtensions
    {
        private static readonly Type ReadOnlyCollectionType = Type.GetType("System.Collections.Generic.IReadOnlyCollection`1", false);

        private static readonly Type ReadOnlyListType = Type.GetType("System.Collections.Generic.IReadOnlyList`1", false);

        public static Type[] GetInterfacesCloseOf(this Type @this, Type closeType)
        {
            if(@this.IsInterface) throw new InvalidOperationException($"TinyIoc: Can't resolve {@this.FullName}");

            return @this.GetInterfaces()
                .Where(t => t.IsGenericTypeDefinedBy(closeType))
                .ToArray();
        }

        public static bool IsGenericTypeDefinedBy(this Type @this, Type openGeneric)
        {
            return !@this.ContainsGenericParameters && @this.IsGenericType && @this.GetGenericTypeDefinition() == openGeneric;
        }

        public static bool IsGenericEnumerableInterfaceType(this Type type)
        {
            return (type.IsGenericTypeDefinedBy(typeof(IEnumerable<>))
                    || type.IsGenericListOrCollectionInterfaceType());
        }

        public static bool IsGenericListOrCollectionInterfaceType(this Type type)
        {
            return type.IsGenericTypeDefinedBy(typeof(IList<>))
                   || type.IsGenericTypeDefinedBy(typeof(ICollection<>))
                   || (ReadOnlyCollectionType != null && type.IsGenericTypeDefinedBy(ReadOnlyCollectionType))
                   || (ReadOnlyListType != null && type.IsGenericTypeDefinedBy(ReadOnlyListType));
        }
    }
}
