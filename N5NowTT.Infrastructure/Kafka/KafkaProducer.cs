using Confluent.Kafka;
using N5NowTT.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace N5NowTT.Infrastructure.Kafka
{
    public class KafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public KafkaProducer(string bootstrapServers, string topic)
        {
            _bootstrapServers = bootstrapServers;
            _topic = topic;
        }

        public async Task ProduceAsync(OperationDTO message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            var messageJson = JsonSerializer.Serialize(message);
            var kafkaMessage = new Message<Null, string> { Value = messageJson };

            var deliveryResult = await producer.ProduceAsync(_topic, kafkaMessage);
            Console.WriteLine($"Produced message to {deliveryResult.TopicPartitionOffset}");
        }
    }
}
