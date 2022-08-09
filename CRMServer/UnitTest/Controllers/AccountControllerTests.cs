using AutoMapper;
using CRMClient;
using CRMServer.Controllers;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Priority;

namespace UnitTest.Controllers
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class AccountControllerTests
    {
        private readonly AccountsController _controller;
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;
        private AccountDTO accountdto;

        public AccountControllerTests(AccountsController controller, CRMService crmService, IMapper mapper)
        {
            _crmService = crmService;
            _controller = controller;
            _mapper = mapper;

            accountdto = new()
            {
                AccountId = new Guid(),
                Name = "BLNBLN",
                Description = "Blnbln IT",
                WebsiteUrl = "Blnbln.net",
                Fax = "0647569654",
            };
        }

        [Fact, Priority(2)]
        public void InsertAccountTest()
        {
            // Arrange
            Account? account = _mapper.Map<Account?>(accountdto);

            // Act
            var result = _controller.InsertAccount(accountdto).Result;

            // Assert
            Assert.NotNull(account);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact, Priority(1)]
        public void GetAccountsTest()
        {
            // Act 
            var result = _controller.GetAccounts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Account>>(result);
            Assert.Equal(_crmService.accounts.GetAllAccounts().Count(), result.Count());
        }

        [Fact, Priority(4)]
        public void GetAccountByIdTest()
        {
            // Arrange
            var testGuid = _crmService.accounts.GetAccountByName(accountdto.Name)?.AccountId;

            // Act
            var result = _controller.GetAccountById((Guid)testGuid);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Account?>>(result);
            Assert.Equal(testGuid, result.Value?.AccountId);
        }

        [Fact, Priority(3)]
        public void GetAccountByNameTest()
        {
            // Arrange 
            string name = accountdto.Name;
            // Act
            var result = _controller.GetAccountByName(name);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Account?>>(result);
            Assert.Equal(name, result.Value?.Name);
        }

        [Fact, Priority(5)]
        public void UpdateAccountTest()
        {
            // Arrange
            var accountdto = new AccountDTO
            {
                Description = "Boulean IT"
            };
            Account? account = _mapper.Map<Account?>(accountdto);

            // Act 
            var result = _controller.UpdateAccount(accountdto);

            // Assert 
            Assert.NotNull(account);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact, Priority(6)]
        public void DeleteAccountTest_NotExistingGuidPassed()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.DeleteAccount(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse);
        }

        [Fact, Priority(7)]
        public void DeleteAccountTest_ExistingGuidPassed()
        {
            // Arrange
            var existingGuid = _crmService.accounts.GetAccountByName(accountdto.Name)?.AccountId;

            // Act
            var noContentResponse = _controller.DeleteAccount((Guid)existingGuid);

            // Assert
            Assert.IsType<OkObjectResult>(noContentResponse);
        }
    }
}
