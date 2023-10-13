using System.Diagnostics;

namespace SortAlgos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region ArraySort

            Random rnd = new Random();
            int[] arr = //{ 1, 4, 3, 3, 9, 8, 7, 2, 5, 0 };
                Enumerable.Repeat(0, 10_000_000)
                          .Select(x => rnd.Next(0, 10_000_000))
                          .ToArray();

            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();

            //ArraySort.BubbleSort(arr);
            //ArraySort.SelectionSort(arr);
            //ArraySort.InsertionSort(arr);
            //ArraySort.RecursiveMergeSort(arr);
            //ArraySort.MergeSort(arr);
            ArraySort.RecursiveQuickSort(arr);
            stopwatch.Stop();
            Console.WriteLine($"Elapsed Time : {stopwatch.ElapsedMilliseconds}");

            //Console.Write("{");
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.Write($"{arr[i]}, ");
            //}
            //Console.WriteLine("}");

            #endregion
        }
    }
}