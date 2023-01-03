using AutoMapper;
using Prototype.Application.Models;
using Prototype.Domain.Entities;
using Prototype.Domain.Enums.Extension;

namespace Prototype.Application.Profiles
{
    public class InvitationProfile : Profile
    {
        public InvitationProfile()
        {
            ContactMapping()
                .ForMember(d=> d.CreateDate, map => map.MapFrom(src=> src.CreatedDate))
                .ForMember(d => d.CategoryDescription, map => map.MapFrom(src => GetCategoryDescription(src)))
                .ForMember(d => d.Address, map => map.MapFrom(src => src.Address));
        }

        private IMappingExpression<Invitation, InvitationResult> ContactMapping()
        {
            return CreateMap<Invitation, InvitationResult>()
                           
                            .ForPath(d => d.Contact.FirstName, map => map.MapFrom(src => GetFirstName(src)))
                            .ForPath(d => d.Contact.FullName, map => map.MapFrom(src => src.Contact.FullName))
                            .ForPath(d => d.Contact.Id, map => map.MapFrom(src => src.Contact.Id))
                            .ForPath(d => d.Contact.PhoneNumber, map => map.MapFrom(src => src.Contact.PhoneNumber))
                            .ForPath(d => d.Contact.CreatedDate, map => map.MapFrom(src => src.Contact.CreatedDate));
        }

        private static string GetFirstName(Invitation src)=>src.Contact.GetFirstName(src.Contact.FullName);

        private static string GetCategoryDescription(Invitation src)=>src.Category.GetDescription();
    }
}
