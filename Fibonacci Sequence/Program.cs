using System;
using System.Diagnostics;

namespace Fibonacci_Sequence
{
    public static class FibonacciSequence
    {
        private static void Main()
        {
            uint n;
            ulong?[] array;

            Console.Write("n = ");
            n = uint.Parse(Console.ReadLine());
            Console.WriteLine();

            array = new ulong?[n + 1];

            Console.WriteLine($"Fibonacci({n}) = {Recursion(n)} (recursion)");
            Console.WriteLine($"Time: {MeasureTime(x => Recursion(x), n)} ms");
            Console.WriteLine();

            Console.WriteLine($"Fibonacci({n}) = {Iteration(n)} (iteration)");
            Console.WriteLine($"Time: {MeasureTime(x => Iteration(x), n)} ms");
            Console.WriteLine();

            Console.WriteLine($"Fibonacci({n}) = {Dynamic(n, array)} (dynamic)");
            Console.WriteLine($"Time: {MeasureTime((x, y) => Dynamic(x, y), n, array)} ms");
            Console.WriteLine();

            Console.WriteLine($"Fibonacci({n}) = {BottomUp(n)} (bottom up)");
            Console.WriteLine($"Time: {MeasureTime(x => BottomUp(x), n)} ms");
            Console.WriteLine();

            Console.ReadKey();
        }

        public static ulong Recursion(uint n)
        {
            if (n < 2)
            {
                return n;
            }
            else
            {
                return Recursion(n - 1) + Recursion(n - 2);
            }
        }

        public static ulong Iteration(uint n)
        {
            if (n < 2)
            {
                return n;
            }
            else
            {
                ulong nthMinus2 = 0, nthMinus1 = 1, nth = 0;

                for (uint i = 2; i <= n; i++)
                {
                    nth = nthMinus1 + nthMinus2;
                    nthMinus2 = nthMinus1;
                    nthMinus1 = nth;
                }

                return nth;
            }
        }

        public static ulong Dynamic(uint n, ulong?[] array)
        {
            if (array[n] != null)
            {
                return (ulong)array[n];
            }
            else
            {
                ulong result;

                if (n < 2)
                {
                    result = n;
                }
                else
                {
                    result = Dynamic(n - 1, array) + Dynamic(n - 2, array);
                }

                array[n] = result;

                return result;
            }
        }

        public static ulong BottomUp(uint n)
        {
            if (n < 2)
            {
                return n;
            }
            else
            {
                ulong?[] array = new ulong?[n + 1];
                array[0] = 0;
                array[1] = 1;

                for (int i = 2; i <= n; i++)
                {
                    array[i] = array[i - 1] + array[i - 2];
                }

                return (ulong)array[n];
            }
        }

        public static long MeasureTime(Action<uint> method, uint n)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            method(n);
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        public static long MeasureTime(Action<uint, ulong?[]> method, uint n, ulong?[] array)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            method(n, array);
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}