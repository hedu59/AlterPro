using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Application.Models
{
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string Queue { get; set; }
    }
}
