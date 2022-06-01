using AutoMapper;
using DataAccess.Models;
using DataAccess.Services.Models;

namespace DataAccess.Services.MapProfiles
{
    internal class DataAccessServicesMapProfile : Profile
    {
        public DataAccessServicesMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
