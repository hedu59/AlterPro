using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Prototype.Domain.Entities;
using System;

namespace Prototype.Application.Filas.Models
{
    public class InvitationMessage
    {
        public InvitationMessage(Invitation invitation, string observation)
        {
            Id = ObjectId.GenerateNewId();
            RegisterDate = DateTime.UtcNow;
            Invitation = invitation;
            Observation = observation;
        }

        [BsonId()]
        public ObjectId Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Observation { get; set; }
        public Invitation Invitation { get; set; }

    }
}
