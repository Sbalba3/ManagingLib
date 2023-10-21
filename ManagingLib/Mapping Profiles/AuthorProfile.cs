using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;

namespace ManagingLib.Mapping_Profiles
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorViewModel, Author>().ReverseMap();
        }
    }
}
