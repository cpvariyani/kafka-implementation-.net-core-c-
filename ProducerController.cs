using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            this._config = config;
        }
        [HttpPost("send")]
        public async Task<ActionResult> Get(string topic, [FromBody]Employee employee)
        {
            string serializedEmployee = JsonConvert.SerializeObject(employee);
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedEmployee });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}