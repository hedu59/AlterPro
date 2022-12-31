using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Prototype.Domain.Enums;
using Prototype.Shared.Commands;
using Prototype.Shared.CustomValidators;
using System.Linq;

namespace Prototype.Application.Commands.Input.Invitation
{
    public class CreateInvitationCommand : Notifiable, IRequest<ICommandResult>
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Email { get; private set; }
        public ECategory Category { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FullName, 10, "Full Name", "The name must be greater or equals than 10 characters")
                .HasMaxLen(FullName, 100, "Full Name", "The name must be fewer or equals than 100 characters")

                .HasMinLen(PhoneNumber, 10, "PhoneNumber", "The PhoneNumber must be greater or equals than 10 characters")
                .HasMaxLen(PhoneNumber, 50, "PhoneNumber", "The PhoneNumber must be fewer or equals than 50 characters")

                .HasMinLen(Number, 1, "Number", "The Number must be greater or equals than 1 characters")
                .HasMaxLen(PhoneNumber, 20, "Number", "The Number must be fewer or equals than 20 characters")

                .HasMinLen(Complement, 100, "Complement", "The Complement must be fewer or equals than 100 characters")

                 .HasMinLen(Neighborhood, 5, "Neighborhood", "The Neighborhood must be fewer or equals than 5 characters")
                 .HasMaxLen(Neighborhood, 250, "Neighborhood", "The Neighborhood must be fewer or equals than 250 characters")

                 .HasMinLen(Street, 1, "Street", "The Street must be fewer or equals than 1 characters")
                 .HasMaxLen(Street, 250, "Street", "The Street must be fewer or equals than 250 characters")

                 .HasMinLen(City, 1, "City", "The City must be fewer or equals than 1 characters")
                 .HasMaxLen(City, 100, "City", "The City must be fewer or equals than 100 characters")

                 .HasMinLen(State, 1, "State", "The State must be fewer or equals than 1 characters")
                 .HasMaxLen(State, 80, "State", "The State must be fewer or equals than 80 characters")

                 .HasMinLen(PostalCode, 1, "PostalCode", "The PostalCode must be fewer or equals than 1 characters")
                 .HasMaxLen(PostalCode, 16, "PostalCode", "The PostalCode must be fewer or equals than 16 characters")

                 .IsEmailOrEmpty(Email, "Email", "The email is invalid")

                 .IsTrue(InviteCategoryValidator(Category), "Category", ValidInviteCategory())

                 .IsGreaterOrEqualsThan(Price, 1, "Price", "The price must be more then 0")

                 .HasMinLen(Description, 1, "Description", "The Description must be fewer or equals than 1 characters")
                 .HasMaxLen(Description, 250, "Description", "The Description must be fewer or equals than 250 characters")

                );

            return Valid;
        }

        public bool InviteCategoryValidator(ECategory category)
        {
            var valoresValidos = new ValidEnumValidator<ECategory>(category).Obter();
            if (valoresValidos.Contains((ECategory)category)) return true;
            return false;
        }

        public string ValidInviteCategory()
        {
            var valoresValidos = new ValidEnumValidator<ECategory>(ECategory.undefined).Obter();
            return $"The {nameof(ECategory)} must be {string.Join(", ", valoresValidos.Select(s => $"{((int)s)} - {s}"))}";
        }


    }
}
