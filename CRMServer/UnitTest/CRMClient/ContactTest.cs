using CRMClient;
using CRMServer.Models.CRM;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace UnitTest.CRMClient {
	[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
	public class ContactTest : CRMBaseTest<Contact> {

		public ContactTest(CRMService crmService, ITestOutputHelper output) : base(crmService, output) {
			CrudFunctions = new CrudFunctions<Contact>(
				crmService.contacts.InsertContact,
				crmService.contacts.GetContactByEmail,
				crmService.contacts.UpdateContact,
				crmService.contacts.DeleteContact
			);
			entity = new Contact() {
				Email = "test@gmail.com",
				Firstname = "test",
				Lastname = "test",
				Birthdate = "2001-03-20",
				Fax = "02952962",
				JobTitle = "Engineer",
				MobilePhone = "06269250265"
			};
		}

		protected override string? SimpleUpdate(ref Contact entityEdited) {
			entityEdited.Firstname = "Test 2";
			return entityEdited.Firstname;
		}

		protected override string? GetUpdatetField(Contact entityEdited) {
			return entityEdited.Firstname;
		}
	}
}
