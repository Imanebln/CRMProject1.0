
using CRMClient;
using CRMServer.Models.CRM;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"C:\Users\Elmes\Desktop\Stage 2022 SQLI\CRMProject1.0\CRMServer\CRMServer\appsettings.json").Build();


CRMService service = new CRMService(config);

Console.WriteLine(service.leads.GetLeadByEmail("test@gmail.com"));