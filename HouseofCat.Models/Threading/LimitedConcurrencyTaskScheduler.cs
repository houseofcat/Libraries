using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HouseofCat.Models.Threading
{
    public class LimitedConcurrencyTaskScheduler : TaskScheduler
    {
        [ThreadStatic]
        private static bool _currentThreadIsProcessingItems;
        private readonly LinkedList<Task> _tasks = new LinkedList<Task>(); // protected by lock(_tasks)

        private readonly int _maxDegreeOfParallelism;
        private int _currentlyProcessing = 0;

        public LimitedConcurrencyTaskScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 2) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));

            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        // Queues a task to the scheduler. 
        protected sealed override void QueueTask(Task task)
        {
            // Add the task to the list of tasks to be processed.  If there aren't enough 
            // delegates currently queued or running to process tasks, schedule another. 
            lock (_tasks)
            {
                _tasks.AddLast(task);
                if (_currentlyProcessing < _maxDegreeOfParallelism)
                {
                    ++_currentlyProcessing;
                    NotifyThreadPoolOfPendingWork();
                }
            }
        }

        // Inform the ThreadPool that there's work to be executed for this scheduler. 
        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                // Note that the current thread is now processing work items.
                // This is necessary to enable inlining of tasks into this thread.
                _currentThreadIsProcessingItems = true;
                try
                {
                    // Process all available items in the queue.
                    while (true)
                    {
                        Task item;
                        lock (_tasks)
                        {
                            // When there are no more items to be processed,
                            // note that we're done processing, and get out.
                            if (_tasks.Count == 0)
                            {
                                --_currentlyProcessing;
                                break;
                            }

                            // Get the next item from the queue
                            item = _tasks.First.Value;
                            _tasks.RemoveFirst();
                        }

                        // Execute the task we pulled out of the queue
                        TryExecuteTask(item);
                    }
                }
                // We're done processing items on the current thread
                finally { _currentThreadIsProcessingItems = false; }
            }, null);
        }

        // Attempts to execute the specified task on the current thread. 
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            var supportInlining = false;

            // If this thread is already processing a task, we may support inlining.
            if (_currentThreadIsProcessingItems)
            {
                // If the task was previously queued, remove it from the queue
                // to try and see if it can run inline.
                if (taskWasPreviouslyQueued && TryDequeue(task))
                { supportInlining = TryExecuteTask(task); }
                else
                { supportInlining = TryExecuteTask(task); }
            }

            return supportInlining;
        }

        // Attempt to remove a previously scheduled task from the scheduler. 
        protected sealed override bool TryDequeue(Task task)
        {
            lock (_tasks) return _tasks.Remove(task);
        }

        // Gets the maximum concurrency level supported by this scheduler. 
        public sealed override int MaximumConcurrencyLevel { get { return _maxDegreeOfParallelism; } }

        // Gets an enumerable of the tasks currently scheduled on this scheduler. 
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(_tasks, ref lockTaken);
                if (lockTaken)
                { return _tasks; }
                else
                { throw new NotSupportedException(); }
            }
            finally
            {
                if (lockTaken) { Monitor.Exit(_tasks); }
            }
        }
    }
}
