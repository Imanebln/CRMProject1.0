using AutoMapper;
using CRMServer.Models.CRM;
namespace CRMServer.DTO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Contact
            CreateMap<ContactDTO, Contact>()
                .ForMember(dest =>
                 dest.Account,
                opt =>
                opt.Ignore()
                )
                .ForMember(dest =>
                dest.BirthdateObj,
                opt =>
                opt.Ignore()
                );

            // Account
            CreateMap<AccountDTO, Account>()
                .ForMember(dest =>
                dest.Contacts,
                opt =>
                opt.Ignore()
                )
                .ForMember(dest =>
                dest.PrimaryContactId,
                opt =>
                opt.Ignore()
                )
                .ForMember(dest =>
                dest.PrimaryContact,
                opt =>
                opt.Ignore()
                );

            // Lead
            CreateMap<LeadDTO, Lead>()
                .ForMember(dest =>
                dest.Account,
                opt =>
                opt.Ignore()
                );

            // Opportunity
            CreateMap<OpportunityDTO, Opportunity>()
                .ForMember(dest =>
                dest.Currency,
                opt =>
                opt.Ignore()
                )
                .ForMember(dest =>
                dest.Account,
                opt =>
                opt.Ignore()
                )
                .ForMember(dest =>
                dest.Contact,
                opt =>
                opt.Ignore()
                );
        }
        
    }
}
