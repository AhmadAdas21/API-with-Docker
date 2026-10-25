namespace API_with_DOCKER.DesignsPatterns.Factory
{
    public class PushNotification:INotification
    {
        private readonly ILogger<PushNotification> _logger;
        public PushNotification(ILogger<PushNotification> logger)
        {
            _logger = logger;
        }
    }
}
