using System.Collections.Concurrent;
using System.Collections.Generic;
using static Prophet.Prelude;

namespace Prophet.VkJournalist
{
    class RoundRobinSequence<T>
    {
        readonly ConcurrentQueue<T> _queue;

        public RoundRobinSequence(IEnumerable<T> items)
        {
            _queue = new ConcurrentQueue<T>(items);
        }

        public Maybe<T> Next()
        {
            if (_queue.TryDequeue(out T next))
            {
                _queue.Enqueue(next);

                return Just(next);
            }

            return Nothing<T>();
        }

        public void Add(T item)
        {
            _queue.Enqueue(item);
        }
    }
}
