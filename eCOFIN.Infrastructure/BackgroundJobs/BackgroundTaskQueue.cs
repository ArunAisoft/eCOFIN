using System.Threading.Channels;

namespace eCOFIN.Infrastructure.BackgroundJobs
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, Task>> _queue;

        public BackgroundTaskQueue()
        {
            _queue = Channel.CreateUnbounded<Func<CancellationToken, Task>>(
                new UnboundedChannelOptions { SingleReader = true });
        }

        public void QueueTask(Func<CancellationToken, Task> task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            _queue.Writer.TryWrite(task);
        }

        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}