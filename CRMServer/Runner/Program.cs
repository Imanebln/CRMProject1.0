
using CRMClient;
using CRMServer.Models.CRM;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder().AddJsonFile(@"C:\Users\Elmes\Desktop\Stage 2022 SQLI\CRMProject1.0\CRMServer\CRMServer\appsettings.json").Build();


CRMService service = new CRMService(config);

Contact contact = new() {
	Email = "test@gmail.com",
	Firstname = "test",
	Lastname = "test",
	Birthdate = "2001-03-20",
	Fax = "02952962",
	JobTitle = "Engineer",
	MobilePhone = "06269250265"
};

Console.WriteLine(service.contacts.GetContactByEmail(contact.Email));
