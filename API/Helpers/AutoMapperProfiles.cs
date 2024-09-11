using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(j => j.Age,d => d.MapFrom(s=>s.DateOfBirth.CalculateAge()))
            .ForMember(x => x.PhotoUrl,
            o =>
                o.MapFrom(s => s.Photos.FirstOrDefault(k => k.IsMain)!.Url));
        CreateMap<Photo, PhotoDto>();
    }
}