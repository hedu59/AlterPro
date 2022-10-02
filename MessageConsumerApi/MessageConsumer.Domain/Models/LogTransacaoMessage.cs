using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Domain.Models
{
    public class LogTransacaoMessage
    {
        public string Id { get; set; }
        public Guid ServidorId { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Observacao { get; set; }
    }
}
