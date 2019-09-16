using System.Collections.Concurrent;
using System.Collections.Generic;
using Prophet.VkJournalist.Model;
using static Prophet.Prelude;

namespace Prophet.VkJournalist
{
    class OwnersSequence
    {
        readonly ConcurrentQueue<Owner> _queue;

        public OwnersSequence(IEnumerable<Owner> owners)
        {
            _queue = new ConcurrentQueue<Owner>(owners);
        }

        public Maybe<Owner> Next()
        {
            if (_queue.TryDequeue(out Owner next))
            {
                _queue.Enqueue(next);

                return Just(next);
            }

            return Nothing<Owner>();
        }

        public void Add(Owner owner)
        {
            _queue.Enqueue(owner);
        }
    }
}
