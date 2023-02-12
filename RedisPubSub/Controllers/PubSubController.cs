using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace RedisPubSub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubSubController : Controller
    {
        private readonly ILogger<PubSubController> _logger;

        public PubSubController(ILogger<PubSubController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "pub")]
        public long Pub(string message)
        {
            var multiflexer = ConnectionMultiplexer.Connect("localhost");
            var pub = multiflexer.GetSubscriber();
            var count = pub.Publish("channel1", message);
            _logger.Log(LogLevel.Information, $"Number of listeners for test {count}");
            _logger.Log(LogLevel.Information, $"message {message}");

            return count;
        }
    }
}
