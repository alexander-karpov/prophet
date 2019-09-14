using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Prophet.VkJournalist
{
    class Program
    {
        static void Main(string[] args)
        {
            using var journalist = new Journalist();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
