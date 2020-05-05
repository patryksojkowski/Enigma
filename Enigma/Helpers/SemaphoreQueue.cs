using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnigmaUI.Helpers
{
    public class SemaphoreQueue
    {
        private readonly ConcurrentQueue<TaskCompletionSource<bool>> _queue =
            new ConcurrentQueue<TaskCompletionSource<bool>>();

        private readonly SemaphoreSlim _semaphore;

        public SemaphoreQueue(int initialCount)
        {
            _semaphore = new SemaphoreSlim(initialCount);
        }

        public SemaphoreQueue(int initialCount, int maxCount)
        {
            _semaphore = new SemaphoreSlim(initialCount, maxCount);
        }

        public void Release()
        {
            _semaphore.Release();
        }

        public void Wait()
        {
            WaitAsync().Wait();
        }

        public Task WaitAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            _queue.Enqueue(tcs);
            _semaphore.WaitAsync().ContinueWith(t =>
            {
                if (_queue.TryDequeue(out TaskCompletionSource<bool> popped))
                {
                    popped.SetResult(true);
                }
            });
            return tcs.Task;
        }
    }
}
