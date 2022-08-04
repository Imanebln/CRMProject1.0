using CRMClient;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace UnitTest {
	public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<ConfigurationProvider>();
            services.AddSingleton<CRMService>((IServiceProvider provider) =>{
                return new CRMService(
                    provider.GetRequiredService<ConfigurationProvider>().Configuration
                );
			});
        }
    }
}
