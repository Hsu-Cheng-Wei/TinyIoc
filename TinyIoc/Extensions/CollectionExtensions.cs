using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyIoc.Extensions
{
    internal static class CollectionExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, Func<TKey, TValue> func)
        {
            if (dic.ContainsKey(key)) return dic[key];

            var val = func(key);

            dic.Add(key, val);

            return val;
        }

        public static object CastByType<T>(this IEnumerable<T> src, Type type)
        {
            var method = typeof(Enumerable).GetMethod("Cast")?
                .MakeGenericMethod(type);

            return method?.Invoke(null, new object[]{ src });
        }
    }
}
