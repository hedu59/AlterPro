using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Prototype.Domain.Entities;
using System;

namespace Prototype.Application.Filas.Models
{
    public class InvitationMessage
    {
        public InvitationMessage(long invitationId, string contactFullName, string contatctPhoneNumber, decimal invitationPrice, string description)
        {
            Id = ObjectId.GenerateNewId();
            RegisterDate = DateTime.UtcNow;
            InvitationId = invitationId;
            InvitationPrice = invitationPrice;
            ContactFullName = contactFullName;
            Description = description;
            ContactPhoneNumber = contatctPhoneNumber;
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
