using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace Prophet.VkJournalist
{
    class Program
    {
        const string ARTICLES_EXCHANGE = "articles";

        static void Main(string[] args)
        {
            using var timer = new Timer(PullUpdates, null, 0, 10000);
            using var db = new VkJournalistContext();
            var factory = new ConnectionFactory()
            {
                HostName = "dialogs.kukuruku.name",
                Port = 5672,
                UserName = "kukuruku",
                Password = "function-tingle-casebook-drier"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: ARTICLES_EXCHANGE, type: "direct");

            var body = Encoding.UTF8.GetBytes("message");

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicPublish(exchange: ARTICLES_EXCHANGE,
                                 routingKey: "vk/{userId}",
                                 basicProperties: props,
                                 body: body);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        static public void PullUpdates(object state)
        {
            Console.WriteLine("Pull updates");
        }
    }
}
