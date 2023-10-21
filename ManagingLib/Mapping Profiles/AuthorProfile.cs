using AutoMapper;
using Managing.DAL.Models;
using Managinglibrary.ViewModels;

namespace Managinglibrary.Mapping_Profiles
{
    public class AuthorProfile: Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorViewModel, Author>().ReverseMap();
        }
    }
}
