using AutoMapper;
using ManagingLib.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ManagingLib.Mapping_Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>().ForMember(d => d.Name, o => o.MapFrom(s => s.RoleName)).ReverseMap();


        }
    }
}
