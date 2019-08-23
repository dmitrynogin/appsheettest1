using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Utils
{
    public static class BottomN
    {
        public static async IAsyncEnumerable<T> Bottom<T, TValue>(this IAsyncEnumerable<T> source, int number, Func<T, TValue> selector)
            where TValue : IComparable<TValue>
        {
            var list = new List<T>();
            await foreach(var item in source)
            {
                list.Insert(list.BestIndex(item, selector), item);
                if (list.Count > number)
                    list.RemoveAt(number);                
            }

            foreach (var item in list)
                yield return item;
        }

        static int BestIndex<T, TValue>(this IList<T> list, T value, Func<T, TValue> selector) 
            where TValue : IComparable<TValue>
        {
            return bestIndex(0, list.Count);
            int bestIndex(int s, int e) =>
                s == e ? s :
                    selector(list[(s + e) / 2]).CompareTo(selector(value)) > 0 
                    ? bestIndex(s, (s + e) / 2) 
                    : bestIndex((s + e) / 2 + 1, e);
        }
    }
}
