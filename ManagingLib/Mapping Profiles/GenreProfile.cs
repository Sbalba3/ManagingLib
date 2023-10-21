using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;

namespace ManagingLib.Mapping_Profiles
{
    public class GenreProfile: Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreViewModel, Genre>().ReverseMap();
        }
    }
}
