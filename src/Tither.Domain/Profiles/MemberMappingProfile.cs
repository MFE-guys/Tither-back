using AutoMapper;
using Tither.Domain.Models;
using Tither.Shared.Requests;
using Tither.Shared.ViewModels;

namespace Tither.Domain.Profiles
{
    public class MemberMappingProfile : Profile
    {
        public MemberMappingProfile() {
            CreateMap<Member, MemberViewModel>();
            CreateMap<RegisterNewMemberRequest, Member>();
            CreateMap<UpdateMemberRequest, Member>();
        }
    }
}
