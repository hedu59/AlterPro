using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums;
using Prototype.Infra.Data.Interfaces;
using Prototype.Infra.Data.Mappings.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Infra.Data.Mappings.Entities
{
    internal class InvitationMap : GenericMap<Invitation>, IEntityTypeConfiguration<Invitation>, IEntityMapping
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            DefaultMap(builder: builder, tableName: "Invitations");

            builder.OwnsOne(
               navigationExpression: e => e.Email,
               buildAction: email =>
               {
                   email.Property(n => n.Valor).HasColumnName("Email").HasMaxLength(100).IsRequired();
               });

            builder.OwnsOne(e => e.Address, address =>
            {
                address.WithOwner();
                address.Property(x => x.Number).HasColumnName("Number").HasMaxLength(20).IsRequired();
                address.Property(x => x.Complement).HasColumnName("Complement").HasMaxLength(100);
                address.Property(x => x.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(250).IsRequired();
                address.Property(x => x.City).HasColumnName("City").HasMaxLength(100).IsRequired();
                address.Property(x => x.State).HasColumnName("State").HasMaxLength(80).IsRequired();
                address.Property(x => x.PostalCode).HasColumnName("PostalCode").HasMaxLength(16).IsRequired();
            });

            builder
                .Property(e => e.Category)
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(e => e.Price)
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(e => e.Status)
                .HasDefaultValue(null);

            builder
                .Property(e => e.CreatedDate)
                .IsRequired();

            builder.HasOne(e=> e.Contact)
                .WithMany()
                .HasForeignKey(e => e.ContactId);

        }
    }
}
