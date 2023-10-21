using AutoMapper;
using ManagingLib.DAL.Models;
using ManagingLib.ViewModels;

namespace ManagingLib.Mapping_Profiles
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            CreateMap<BookViewModel, Book>().ReverseMap();

        }
    }
}
