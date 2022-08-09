using AutoMapper;
using CRMClient;
using CRMServer.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using CRMServer.DTO;
using CRMServer.Services;

namespace UnitTest {
	public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<ConfigurationProvider>();
            services.AddSingleton<CRMService>((IServiceProvider provider) =>{
                return new CRMService(
                    provider.GetRequiredService<ConfigurationProvider>().Configuration
                );
			});

            services.AddSingleton<IMapper>((IServiceProvider provider) =>
            {
                return new MapperConfiguration(config =>
                {
                    config.AddProfile<AutoMapperProfile>();
                }).CreateMapper();
            });

            services.AddSingleton<ContactsController>((IServiceProvider provider) =>
            {
                return new ContactsController(provider.GetRequiredService<CRMService>(), provider.GetRequiredService<IMapper>());
            });

            services.AddSingleton<AccountsController>((IServiceProvider provider) =>
            {
                return new AccountsController(provider.GetRequiredService<CRMService>(), provider.GetRequiredService<IMapper>());
            });

            services.AddSingleton<LeadsController>((IServiceProvider provider) =>
            {
                return new LeadsController(provider.GetRequiredService<CRMService>(), provider.GetRequiredService<IMapper>());
            });

            services.AddSingleton<OpportunitysController>((IServiceProvider provider) =>
            {
                return new OpportunitysController(provider.GetRequiredService<CRMService>(), provider.GetRequiredService<IMapper>());
            });

           
        }
    }
}
