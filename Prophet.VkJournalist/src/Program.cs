using System;
using System.Threading;

namespace Prophet.VkJournalist
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancelEvent = new AutoResetEvent(initialState: false);
            using var journalist = new Journalist();

            Console.WriteLine("Application started. Press Ctrl+C to shut down.");

            Console.CancelKeyPress += (s, e) => cancelEvent.Set();
            cancelEvent.WaitOne();
        }
    }
}
