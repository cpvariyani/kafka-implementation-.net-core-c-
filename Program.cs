using Confluent.Kafka;
using System;

namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("temp-topic-demo");
                while (true)
                {
                    var cr = consumer.Consume();
                    Console.WriteLine(cr.Message.Value);
                }
            }
        }
    }
}
