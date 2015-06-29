using System.Collections.Concurrent;

namespace WepApiExceptionPipeline.Extensions
{
    internal static class ConcurrentDictionaryExtensions
    {
        public static bool TryReplace<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> d, TKey key, TValue value)
        {
            TValue _;
            d.TryRemove(key, out _);
            return d.TryAdd(key, value);
        }
    }
}