using AutoMapper;
using CRMServer.Models.CRM;
namespace CRMServer.DTO
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDTO, Contact>();
            CreateMap<AccountDTO, Account>();
        }
        
    }
}
