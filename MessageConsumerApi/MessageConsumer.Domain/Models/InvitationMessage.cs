using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Domain.Models
{
    public class InvitationMessage
    {
        public string Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Description { get; set; }
        public long InvitationId { get; set; }
        public string ContactFullName { get; set; }
        public string ContactPhoneNumber { get; set; }     
        public decimal InvitationPrice { get; set; }
    }
}
