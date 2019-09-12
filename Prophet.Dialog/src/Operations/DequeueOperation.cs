using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using static Prophet.Prelude;

namespace Prophet.Dialog.Operations
{
    public class DequeueOperation
    {
        public Maybe<string> Dequeue(string queueName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "dialogs.kukuruku.name",
                Port = 5672,
                UserName = "kukuruku",
                Password = "function-tingle-casebook-drier"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var args = new Dictionary<string, object>();
            var week = 1000 * 60 * 60 * 24 * 7;
            args.Add("x-message-ttl", week);

            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: args
            );

            var message = channel.BasicGet(queue: queueName, autoAck: true);

            if (message == null)
            {
                return Nothing<string>();
            }

            var messageText = Encoding.UTF8.GetString(message.Body);
            return Just(messageText);
        }
    }
}
