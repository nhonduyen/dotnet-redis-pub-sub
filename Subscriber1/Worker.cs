using StackExchange.Redis;

namespace Subscriber1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var multiflexer = ConnectionMultiplexer.Connect("localhost");
            var sub = multiflexer.GetSubscriber();
            sub.Subscribe("channel1", (channel, message) =>
            {
                //Output received message
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}]: --Subscriber1-- {$"Message {message} received successfully"}");
            });
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}