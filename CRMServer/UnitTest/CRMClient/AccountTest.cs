using CRMClient;
using CRMServer.Models.CRM;
using System;
using Xunit.Abstractions;

namespace UnitTest.CRMClient {
	public class AccountTest : CRMBaseTest<Account> {
		public AccountTest(CRMService crmService, ITestOutputHelper output) : base(crmService, output) {
			CrudFunctions = new CrudFunctions<Account>(
				crmService.accounts.InsertAccount,
				crmService.accounts.GetAccountByName,
				crmService.accounts.UpdateAccount,
				crmService.accounts.DeleteAccount
			);
			entity = new Account(){
				Name = "SOP Inc.",
				WebsiteUrl = "www.sop.com",
				Fax = "2222",
				Description = "This is a description",
			};
		}

		protected override string? SimpleUpdate(ref Account entityEdited) {
			entityEdited.Fax = "1111";
			return entityEdited.Fax;
		}

		protected override string? GetUpdatetField(Account entityEdited) {
			return entityEdited.Fax;
		}

		
	}
}
