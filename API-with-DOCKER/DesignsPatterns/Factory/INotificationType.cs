namespace API_with_DOCKER.DesignsPatterns.Factory
{
    public interface INotificationType
    {
        Task SendAsync(string destination, string message,CancellationToken cancellationToken = default);
    }
}
