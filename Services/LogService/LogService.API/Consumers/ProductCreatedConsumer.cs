using MassTransit;
using ECommerceTask.Core.Events;



namespace LogService.API.Consumers
{

    public class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedConsumer> _logger;

        public ProductCreatedConsumer(ILogger<ProductCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation($"[RabbitMQ] Yeni ürün yakalandý! ID: {message.Id}, Ýsim: {message.Name}, Tarih: {message.CreatedDate}");

            return Task.CompletedTask;
        }
    }
}