using AutoMapper;
using GooglePay.DTOs;
using GooglePay.Models;

namespace GooglePay.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles() 
        {
            CreateMap<RegisterDto, AccountHolder>();
            CreateMap<AccountHolder, MemberDto>();
        }
    }
}
