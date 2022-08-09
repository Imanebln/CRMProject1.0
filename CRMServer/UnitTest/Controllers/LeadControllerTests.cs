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
    public class LeadControllerTests
    {
        private readonly LeadsController _controller;
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;
        private LeadDTO leaddto;

        public LeadControllerTests(LeadsController controller, CRMService crmService, IMapper mapper)
        {
            _controller = controller;
            _crmService = crmService;
            _mapper = mapper;

             leaddto = new()
             {
                LeadId = new Guid(),
                Firstname = "Lead1",
                Lastname = "Lead1",
                Address = "lead1Adress",
                Email = "lead1@gmail.com",
                JobTitle = "Lead1job",
                Subject = "lead1Subject"
             };
        }

        [Fact, Priority(1)]
        public void GetLeadsTest()
        {
            // Act
            var result = _controller.GetAllLeads();

            // Assert
            Assert.NotNull(result);
            var leads = Assert.IsType<List<Lead>>(result);
            Assert.Equal(_crmService.leads.GetAllLeads().Count(), leads.Count);
        }
        
        [Fact, Priority(4)]
        public void GetLeadByIdTest()
        {
            // Arrange
            var testGuid = _crmService.leads.GetLeadByEmail(leaddto.Email)?.LeadId;

            // Act
            var result = _controller.GetLeadById((Guid)testGuid);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Lead?>>(result);
            Assert.Equal(testGuid, result.Value?.LeadId);
        }

        [Fact, Priority(3)]
        public void GetLeadByEmailTest()
        {
            // Act
            var result = _controller.GetLeadByEmail(leaddto.Email);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Lead?>>(result);
            Assert.Equal(leaddto.Email, result.Value?.Email);
        }

        [Fact, Priority(2)]
        public void InsertLeadTest()
        {
            // Arrange
            Lead? lead = _mapper.Map<Lead>(leaddto);

            // Act
            var result = _controller.InsertLead(leaddto);

            // Assert
            Assert.NotNull(lead);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact, Priority(5)]
        public void UpdateLeadTest()
        {
            leaddto.JobTitle = "Architect";
            Lead? lead = _mapper.Map<Lead>(leaddto);

            // Act
            var result = _controller.UpdateLead(leaddto);

            // Assert
            Assert.NotNull(lead);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact, Priority(6)]
        public void DeleteLeadTest_NotExistingGuidPassed()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.DeleteLead(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(badResponse);
        }

        [Fact, Priority(7)]
        public void DeleteLeadTest_ExistingGuidPassed()
        {
            // Arrange
            var existingGuid = _crmService.leads.GetLeadByEmail(leaddto.Email)?.LeadId;

            // Act
            var noContentResponse = _controller.DeleteLead((Guid)existingGuid);

            // Assert
            Assert.IsType<OkObjectResult>(noContentResponse);
        }

    }
}
