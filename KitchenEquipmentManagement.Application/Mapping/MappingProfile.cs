using AutoMapper;
using KitchenEquipmentManagement.Application.DTOs;
using KitchenEquipmentManagement.Domain.Models;

namespace KitchenEquipmentManagement.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User → UserDto (combine first and last name)
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}".Trim()));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src =>
                    src.FullName.Contains(" ") ? src.FullName.Split(new[] { ' ' }, StringSplitOptions.None)[0] : src.FullName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src =>
                    src.FullName.Contains(" ") ? string.Join(" ", src.FullName.Split(new[] { ' ' }, StringSplitOptions.None).Skip(1)) : ""))
                .ForMember(dest => dest.Sites, opt => opt.Ignore())
                .ForMember(dest => dest.Equipments, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, SignupRequest>().ReverseMap();
            CreateMap<User, LoginRequest>().ReverseMap();
            CreateMap<Site, SiteDto>().ReverseMap();
            CreateMap<Equipment, EquipmentDto>().ReverseMap();
            CreateMap<Equipment, EquipmentRequest>().ReverseMap();
            CreateMap<RegisteredEquipment, RegisterEquipmentRequest>().ReverseMap();
        }
    }
}
