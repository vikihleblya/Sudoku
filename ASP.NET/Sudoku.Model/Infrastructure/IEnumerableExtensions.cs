using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Infrastructure
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            var r = new Random();
            return list
                .Select(x => new { Number = r.Next(), Item = x })
                .OrderBy(x => x.Number)
                .Select(x => x.Item);
        }
    }
}