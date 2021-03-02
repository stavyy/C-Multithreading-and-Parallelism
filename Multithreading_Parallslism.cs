// Stavros Avdella
// Multithreading and Parallelism


using System;
using System.Diagnostics;
using System.Threading;

namespace Multithreading_Parallslism
{
    class Program
    {
        static int[] data = new int[10000000];
        static Thread[] thrd = new Thread[4];

        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            calc(0, data.Length);
            sw.Stop();
            Console.WriteLine("Sequentially, calc takes {0} milliseconds to run.", Math.Truncate(sw.Elapsed.TotalMilliseconds));

            int steps = data.Length / thrd.Length;

            Stopwatch sw2 = Stopwatch.StartNew();

            for (int i = 0; i < thrd.Length; i++)
            {
                int j = i;
                thrd[j] = new Thread(() => calc(j * steps, steps));
                thrd[j].Start();
            }

            for (int i = 0; i < thrd.Length; i++)
            {
                thrd[i].Join();
            }

            sw2.Stop();
            Console.WriteLine("Multithreaded, calc takes {0} milliseconds to run.", Math.Truncate(sw2.Elapsed.TotalMilliseconds));
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        static void calc(int startIndex, int reps)
        {
            for (int i = startIndex; i < startIndex + reps; i++)
            {
                data[i] = (int)(Math.Atan(i) * Math.Acos(i) * Math.Cos(i) * Math.Sin(i));
            }
        }
    }
}