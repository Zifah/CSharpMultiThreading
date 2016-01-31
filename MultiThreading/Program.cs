using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            //AsyncDelegate();

            ThreadPool.SetMinThreads(1000, 1000);

            var currentThread = Thread.CurrentThread;

            var tasks = new List<Task>();

            Console.WriteLine("Started {0}", DateTime.Now);

            for (int i = 0; i < 5; i++)
            {
                var task = Task<string>.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("He is a naughty boy!!!");
                    return "Done!!!";
                });

                tasks.Add(task);
            }

            foreach (var task in tasks)
            {
                task.Wait();
            }

            Console.WriteLine("Ended {0}", DateTime.Now);
        }

        private static void AsyncDelegate()
        {
            Func<string, int> method = GetLength;
            var worker = method.BeginInvoke("Cookies", null, null);
            var length = method.EndInvoke(worker);
        }

        private static int GetLength(string theString)
        {
            return theString.Length;
        }
    }
}
