using AutoMapper;
using CRMClient;
using CRMServer.Controllers;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;
using Xunit.Priority;

namespace UnitTest.Controllers
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class ContactControllerTests
    {
        private readonly ContactsController _controller;
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;
        private ContactDTO contactdto;

        public ContactControllerTests(ContactsController controller,CRMService crmService, IMapper mapper)
        {
            _crmService = crmService;
            _controller = controller;
            _mapper = mapper;

            contactdto = new()
            {
                ContactId = new Guid(),
                Firstname = "Mohammed",
                Lastname = "Kadiri",
                Birthdate = "2022-08-05",
                Email = "kadiri@gmail.com",
                JobTitle = "Developer",
                MobilePhone = "0645789631"
            };
        }

        [Fact, Priority(2)]
        public void InsertContactTest()
        {
            // Arrange
            Contact? contact = _mapper.Map<Contact?>(contactdto);
            
            // Act
            var result = _controller.InsertContact(contactdto).Result;

            // Assert
            Assert.NotNull(contact);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact, Priority(1)]
        public void GetContactsTest()
        {
            // Act 
            var result = _controller.GetContacts() as IEnumerable<Contact>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Contact>>(result);
            Assert.Equal(_crmService.contacts.GetAllContacts().Count(), result.Count());
        }

        [Fact, Priority(4)]
        public void GetContactByIdTest()
        {
            // Arrange
            var testGuid = _crmService.contacts.GetContactByEmail(contactdto.Email)?.ContactId;

            // Act
            var result = _controller.GetContactByIdPrimary((Guid)testGuid);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Contact?>>(result);
            Assert.Equal(testGuid, result.Value?.ContactId);
        }

        [Fact, Priority(3)]
        public void GetContactByEmailTest()
        {
            // Act
            var result = _controller.GetContactByEmailPrimary(contactdto.Email);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Contact?>>(result);
            Assert.Equal(contactdto.Email, result.Value?.Email);
        }

        [Fact, Priority(5)]
        public void UpdateContactTest()
        {
            // Arrange
            var contactdto = new ContactDTO
            {
                JobTitle = "Architect",
            };
            Contact? contact = _mapper.Map<Contact?>(contactdto);

            // Act 
            var result = _controller.UpdateContact(contactdto);

            // Assert 
            Assert.NotNull(contact);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact, Priority(6)]
        public void DeleteContactTest_NotExistingGuidPassed()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.DeleteContact(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse);
        }

        [Fact, Priority(7)]
        public void DeleteContactTest_ExistingGuidPassed()
        {
            // Arrange
            var existingGuid = _crmService.contacts.GetContactByEmail(contactdto.Email)?.ContactId;

            // Act
            var noContentResponse = _controller.DeleteContact((Guid)existingGuid);

            // Assert
            Assert.IsType<OkObjectResult>(noContentResponse);
        }
    }
}
