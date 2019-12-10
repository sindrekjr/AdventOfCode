/**
 * This utility class is largely based on:
 * https://github.com/jeroenheijmans/advent-of-code-2018/blob/master/AdventOfCode2018/Util.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions {

    public static class Utilities {

        public static int[] ToIntArray(this string str, string delimiter = "") {
            if(delimiter == "") {
                var result = new List<int>(); 
                foreach(char c in str) if(int.TryParse(c.ToString(), out int n)) result.Add(n); 
                return result.ToArray(); 
            } else {
                return str
                    .Split(delimiter)
                    .Where(n => int.TryParse(n, out int v))
                    .Select(n => Convert.ToInt32(n))
                    .ToArray();
            }
        }

        public static long[] ToLongArray(this string str, string delimiter = "") {
            if(delimiter == "") {
                var result = new List<long>(); 
                foreach(char c in str) if(long.TryParse(c.ToString(), out long n)) result.Add(n); 
                return result.ToArray(); 
            } else {
                return str
                    .Split(delimiter)
                    .Where(n => long.TryParse(n, out long v))
                    .Select(n => Convert.ToInt64(n))
                    .ToArray();
            }
        }

        public static int MinOfMany(params int[] items) {
            var result = items[0];
            for (int i = 1; i < items.Length; i++) {
                result = Math.Min(result, items[i]);
            }
            return result;
        }

        public static int MaxOfMany(params int[] items) {
            var result = items[0];
            for (int i = 1; i < items.Length; i++) {
                result = Math.Max(result, items[i]);
            }
            return result;
        }

        // https://stackoverflow.com/a/3150821/419956 by @RonWarholic
        public static IEnumerable<T> Flatten<T>(this T[,] map) {
            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {
                    yield return map[row, col];
                }
            }
        }

        public static string JoinAsStrings<T>(this IEnumerable<T> items) {
            return string.Join("", items);
        }

        public static string[] SplitByNewline(this string input, bool shouldTrim = false) {
            return input
                .Split(new[] {"\r", "\n", "\r\n"}, StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => shouldTrim ? s.Trim() : s)
                .ToArray();
        }

        public static string Reverse(this string str) {
            char[] arr = str.ToCharArray(); 
            Array.Reverse(arr); 
            return new string(arr); 
        }

        public static int ManhattanDistance((int x, int y) a, (int x, int y) b) {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y); 
        }

        public static int FindGCD(int a, int b) {
            while(a != b) {
                if(a > b) a = a - b; 
                if(b > a) b = b - a; 
            }
            return a; 
        }

        public static void Repeat(this Action action, int count) {
            for(int i = 0; i < count; i++) action(); 
        }
        
        // https://github.com/tslater2006/AdventOfCode2019
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values) {
            return (values.Count() == 1) ? new[]{values} : values.SelectMany(v => Permutations(values.Where(x => x.Equals(v) == false)), (v, p) => p.Prepend(v));
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> array, int size) {
            for(var i = 0; i < (float) array.Count() / size; i++) {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}