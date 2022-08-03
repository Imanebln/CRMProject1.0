using AutoMapper;
using CRMServer.Models.CRM;
namespace CRMServer.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContactDTO, Contact>();
            CreateMap<AccountDTO, Account>();
            CreateMap<LeadDTO, Lead>();
            CreateMap<OpportunityDTO, Opportunity>();
        }
        
    }
}
