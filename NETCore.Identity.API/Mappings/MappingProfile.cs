using AutoMapper;
using NETCore.Identity.API.Resources;
using NETCore.Identity.Core.Models;

namespace NETCore.Identity.API.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<UserSignUpResource, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(ur => ur.Email));
        }
    }
}
