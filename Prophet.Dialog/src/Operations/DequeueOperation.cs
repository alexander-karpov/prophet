using System;

namespace Prophet.Dialog.Operations
{
    public class DequeueOperation : IDisposable
    {
        MessageQueue _mq = new MessageQueue();

        public Maybe<string> Dequeue(string queue)
        {
            _mq.EnsureQueue(queue);

            return _mq.Consume(queue);
        }

        public void Dispose()
        {
            _mq.Dispose();
        }
    }
}
