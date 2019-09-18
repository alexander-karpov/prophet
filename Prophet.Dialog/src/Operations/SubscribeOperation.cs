using System;

namespace Prophet.Dialog.Operations
{
    public class SubscribeOperation : IDisposable
    {
        MessageQueue _mq = new MessageQueue();

        public void Subscribe(string userId, string blogId)
        {
            _mq.EnsureQueue(queue: userId);
            _mq.SubscribeToPosts(queue: userId, routingKey: $"vk/{blogId}");
        }

        public void Dispose()
        {
            _mq.Dispose();
        }
    }
}
