
using CRMClient;
using CRMServer.Models.CRM;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"C:\Users\Elmes\Desktop\Stage 2022 SQLI\CRMProject1.0\CRMServer\CRMServer\appsettings.json").Build();


CRMService service = new CRMService(config);

Opportunity? opp = service.opportunities.GetOpportunityById(Guid.Parse("e90a0493-e8f0-ea11-a815-000d3a1b14a2"));

opp.Name = "10 Airpot XL Coffee Makers for Alpine Ski House";

opp = service.opportunities.UpdateOpportunity(opp).Result;

Console.WriteLine(opp);