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
    public class LeadsController : ControllerBase
    {
        private readonly CRMService _crmService;
        private readonly IMapper _mapper;

        public LeadsController(CRMService crmService,IMapper mapper)
        {
            _crmService = crmService;
            _mapper = mapper;
        }

        // GET: api/Leads
        [HttpGet]
        public IEnumerable<Lead?> GetAllLeads()
        {
            return _crmService.leads.GetAllLeads();
        }

        // GET: api/Leads/ById
        [HttpGet("{id}")]
        public ActionResult<Lead?> GetLeadById(Guid id)
        {
            return _crmService.leads.GetLeadById(id);
        }

        // GET: api/Leads/ByEmail
        [HttpGet("GetLeadByEmail")]
        public ActionResult<Lead?> GetLeadByEmail(string email)
        {
            return _crmService.leads.GetLeadByEmail(email);
        }

        // GET: api/Leads/Where
        [HttpGet("GetLeadWhere")]
        public IEnumerable<Lead?> GetLeadWhere(LeadParameters lead)
        {
            return _crmService.leads.GetLeadsWhere(lead);
        }

        // PUT: api/Leads
        [HttpPut("UpdateLead")]
        public IActionResult UpdateLead(LeadDTO leaddto)
        {
            Lead? lead = _mapper.Map<Lead>(leaddto);
            lead = _crmService.leads.UpdateLead(lead).Result;
            if (lead == null)
            {
                return NotFound("This lead does not exist!");
            }
            return Ok(lead);
        }

        // POST: api/Leads
        [HttpPost("InsertLead")]
        public IActionResult InsertLead(LeadDTO leaddto)
        {
            Lead? lead = _mapper.Map<Lead>(leaddto);
            lead = _crmService.leads.InsertLead(lead).Result;
            if(lead == null) 
            {
                return BadRequest("This lead already exists!");
            }
            return Ok(lead);
        }

        // DELETE: api/Lead
        [HttpDelete("DeleteLead")]
        public IActionResult DeleteLead(Guid id)
        {
            Lead? lead = _crmService.leads.GetLeadById(id);
            if (lead == null)
            {
                return NotFound("This lead does not exists!");
            }
            _ = _crmService.leads.DeleteLead(lead).Result;
            return Ok("Lead deleted successfully!");
        }
    }
}
