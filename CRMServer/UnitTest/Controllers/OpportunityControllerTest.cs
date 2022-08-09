using AutoMapper;
using CRMClient;
using CRMServer.Controllers;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace UnitTest.Controllers
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class OpportunityControllerTest
    {
        public readonly OpportunitysController _controller;
        public readonly IMapper _mapper;
        public readonly CRMService _crmService;
        public OpportunityDTO opportunitydto;
        public ITestOutputHelper output;

        public OpportunityControllerTest(OpportunitysController controller, IMapper mapper, CRMService crmservice, ITestOutputHelper output)
        {
            _controller = controller;
            _mapper = mapper;
            _crmService = crmservice;
            this.output = output;

            opportunitydto = new()
            {
                OpportunityId = new Guid(),
                Name = "Opportunity1",
                Description = "Opportunity1",
                Email = "opportunity1@gmail.com",
                StepName = "Opportunity1",
                CloseProbability = 1,
                CurrentSituation = "Opportunity1",
                CustomerNeed = "Opportunity1",
                TotalAmount = 10,
                ProposedSolution = "Opportunity1",
                CreatedOn = "2022-08-06",
                EstimatedClosedate = "2022-09-06",
                EstimatedValue = 1
            };
        }

        [Fact,  Priority(1)]
        public void GetOpportunitiesTest()
        {
            // Act
            var result = _controller.GetAllOpportunities();
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(_crmService.opportunities.GetAllOpportunities().Count(), result.Count());
        }

        [Fact, Priority(4)]
        public void GetOpportunityByIdTest()
        {
            // Arrange
            var testGuid = _crmService.opportunities.GetOpportunityByEmail(opportunitydto.Email)?.OpportunityId;

            // Act
            var result = _controller.GetOpportunityById((Guid)testGuid);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testGuid, result.Value?.OpportunityId);
            Assert.IsType<ActionResult<Opportunity?>>(result);
        }

        [Fact, Priority(3)]
        public void GetOpportunitybyEmailTest()
        {
            output.WriteLine("Email : " + opportunitydto.Email);
            // Act
            var result = _controller.GetOpportunityByEmail(opportunitydto.Email);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ActionResult<Opportunity?>>(result);
            Assert.Equal(opportunitydto.Email, result.Value?.Email);
        }

        [Fact, Priority(2)]
        public void InsertOpportunityTest()
        {
            // Arrange
            Opportunity? opportunity = _mapper.Map<Opportunity>(opportunitydto);
            output.WriteLine(opportunity.ToString());
            // Act 
            var result = _controller.InsertOpportunity(opportunitydto);

            // Assert
            Assert.NotNull(opportunity);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact, Priority(5)]
        public void UpdateOpportunityTest()
        {
            // Arrange
            opportunitydto.Description = "Very useless description";
            Opportunity? opportunity = _mapper.Map<Opportunity>(opportunitydto);

            // Act
            var result = _controller.UpdateOpportunity(opportunitydto);

            // Assert
            Assert.NotNull(opportunity);
            //Assert.IsType<OkObjectResult>(result);
        }

        [Fact, Priority(6)]
        public void DeleteOpportunityTest_NotExistingGuidPassed()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var result = _controller.DeleteOpportunity(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact, Priority(7)]
        public void DeleteOpportunityTest_ExistingGuidPassed()
        {
            // Arrange
            var existingGuid = _crmService.opportunities.GetOpportunityByEmail(opportunitydto.Email)?.OpportunityId;

            // Act
            var result = _controller.DeleteOpportunity((Guid)existingGuid);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
