using System;
using System.Text;
using static Prophet.Prelude;
using RabbitMQ.Client;
using System.Collections.Generic;

namespace Prophet
{
    public class MessageQueue : IDisposable
    {
        const string POSTS_EXCHANGE = "posts";

        IConnection _connection;
        IModel _channel;

        public MessageQueue()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "dialogs.kukuruku.name",
                Port = 5672,
                UserName = "kukuruku",
                Password = "function-tingle-casebook-drier"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: POSTS_EXCHANGE, type: "direct");
        }

        public void EnsureQueue(string queue)
        {
            var args = new Dictionary<string, object>();
            var week = 1000 * 60 * 60 * 24 * 7;
            args.Add("x-message-ttl", week);

            _channel.QueueDeclare(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: args
            );
        }

        public void SubscribeToPosts(string queue, string routingKey)
        {
            _channel.QueueBind(
                queue: queue,
                exchange: POSTS_EXCHANGE,
                routingKey: routingKey);
        }

        public void PublishPost(string message, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);

            var props = _channel.CreateBasicProperties();
            props.Persistent = true;

            _channel.BasicPublish(
                exchange: POSTS_EXCHANGE,
                routingKey: routingKey,
                basicProperties: props,
                body: body);
        }

        public Maybe<string> Consume(string queue)
        {
            var message = _channel.BasicGet(queue: queue, autoAck: true);

            if (message == null)
            {
                return Nothing<string>();
            }

            return Just(Encoding.UTF8.GetString(message.Body));
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
