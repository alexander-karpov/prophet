using System;
using System.Threading;
using System.Threading.Tasks;

namespace Prophet.VkJournalist
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var journalist = new Journalist();
            var cancellationSource = new CancellationTokenSource();
            var token = cancellationSource.Token;

            Console.WriteLine("Application started. Press Ctrl+C to shut down.");
            Console.CancelKeyPress += (s, e) => cancellationSource.Cancel();

            await Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await journalist.PullAndPublishUpdates(null);
                    Thread.Sleep(10_000);
                }
            });
        }
    }
}
