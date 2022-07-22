using CRMClient;
using CRMServer.Models.Parameters;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
							.AddJsonFile(@"C:\Users\Elmes\Desktop\Stage 2022 SQLI\CRMProject1.0\CRMServer\CRMServer\appsettings.json")
							.Build();
CRMService service = new CRMService(configuration);

AccountParameters parameters = new AccountParameters() { AccountId = Guid.Parse("88cea450-cb0c-ea11-a813-000d3a1b1223") };

// 259