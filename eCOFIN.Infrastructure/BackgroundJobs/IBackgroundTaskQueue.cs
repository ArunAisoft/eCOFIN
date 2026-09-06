namespace eCOFIN.Infrastructure.BackgroundJobs
{
    public interface IBackgroundTaskQueue
    {
        void QueueTask(Func<CancellationToken, Task> task);
        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}