using AutoMapper;
using CRMClient;
using CRMServer.DTO;
using CRMServer.Models.CRM;
using CRMServer.Models.Parameters;
using Microsoft.AspNetCore.Mvc;


namespace CRMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunitysController : ControllerBase
    {
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;

        public OpportunitysController(CRMService crmService, IMapper mapper)
        {
            _crmService = crmService;
            _mapper = mapper;
        }

        // GET: api/Opportunities
        [HttpGet]
        public IEnumerable<Opportunity?> GetAllOpportunitiesGetAllOpportunities()
        {
            return _crmService.opportunities.GetAllOpportunities();
        }

        // GET: api/Opportunities/ById
        [HttpGet("{id}")]
        public ActionResult<Opportunity?> GetOpportunityById(Guid id)
        {
            return _crmService.opportunities.GetOpportunityById(id);
        }

        // GET: api/Opportunities/ByEmail
        [HttpGet("GetOpportunityByEmail")]
        public ActionResult<Opportunity?> GetOpportunityByEmail(string email)
        {
            return _crmService.opportunities.GetOpportunityByEmail(email);
        }

        // GET: api/Opportunities/Where
        [HttpGet("GetOpportunityWhere")]
        public IEnumerable<Opportunity?> GetOpportunityWhere(OpportunityParameters pportunity)
        {
            return _crmService.opportunities.GetOpportunitiesWhere(pportunity);
        }

        // PUT: api/Opportunities
        [HttpPut("UpdateOpportunity")]
        public IActionResult UpdateOpportunity(OpportunityDTO opportunitydto)
        {
            Opportunity? opportunity = _mapper.Map<Opportunity>(opportunitydto);
            opportunity = _crmService.opportunities.UpdateOpportunity(opportunity).Result;
            if (opportunity == null)
            {
                return NotFound("This opportunity does not exist!");
            }
            return Ok(opportunity);
        }

        // POST: api/Opportunities
        [HttpPost("InsertOpportunity")]
        public IActionResult InsertOpportunity(OpportunityDTO opportunitydto)
        {
            Opportunity? opportunity = _mapper.Map<Opportunity>(opportunitydto);
            opportunity = _crmService.opportunities.InsertOpportunity(opportunity).Result;
            if (opportunity == null)
            {
                return BadRequest("This opportunity already exists!");
            }
            return Ok(opportunity);
        }

        // DELETE: api/Opportunities
        [HttpDelete("DeleteOpportunity")]
        public IActionResult DeleteOpportunity(Guid id)
        {
            Opportunity? opportunity = _crmService.opportunities.GetOpportunityById(id);
            if (opportunity == null)
            {
                return NotFound("This opportunity does not exists!");
            }
            _ = _crmService.opportunities.DeleteOpportunity(opportunity).Result;
            return Ok("Opportunity deleted successfully!");
        }
    }
}
