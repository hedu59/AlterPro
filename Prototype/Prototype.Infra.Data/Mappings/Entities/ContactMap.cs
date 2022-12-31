using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prototype.Domain.Entities;
using Prototype.Infra.Data.Interfaces;
using Prototype.Infra.Data.Mappings.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Infra.Data.Mappings.Entities
{
    internal class ContactMap : GenericMap<Contact>, IEntityTypeConfiguration<Contact>, IEntityMapping
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            DefaultMap(builder: builder, tableName: "Contacts");

            builder
                .Property(x => x.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
