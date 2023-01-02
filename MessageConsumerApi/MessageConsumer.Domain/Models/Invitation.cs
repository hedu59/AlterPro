using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessageConsumer.Application.Models
{
    public class Invitation
    {
        public Invitation()
        {
            this.Id = ObjectId.GenerateNewId();
            this.RegisterDate = DateTime.UtcNow;
        }

        [BsonId()]
        public ObjectId Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Description { get; set; }
        public long InvitationId { get; set; }
        public string ContactFullName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public decimal InvitationPrice { get; set; }

    }
}
