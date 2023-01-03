using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Application.Models
{
    public class InvitationResult
    {
        public DateTime CreateDate { get; set; }
        public long Id { get; set; }
        public  long ContactId { get;  set; }
        public Contact Contact { get;  set; }
        public Address Address { get;  set; }
        public Email Email { get;  set; }
        public ECategory Category { get;  set; }
        public string CategoryDescription { get;  set; }
        public decimal Price { get;  set; }
        public string Description { get;  set; }
        public bool? Status { get;  set; }
    }
}
