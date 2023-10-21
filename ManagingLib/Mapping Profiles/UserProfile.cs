using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;

namespace ManagingLib.Mapping_Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();

        }

    }
}
