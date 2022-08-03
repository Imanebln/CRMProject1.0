using Microsoft.AspNetCore.Mvc;
using CRMServer.Models.CRM;
using CRMClient;
using AutoMapper;
using CRMServer.DTO;

namespace CRMServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly CRMService _crmService;
        private readonly IMapper _mapper;

        public ContactsController(CRMService crmService,IMapper mapper)
        {
            _crmService = crmService;
            _mapper = mapper; 
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {
            return _crmService.contacts.GetAllContacts();
        }

        // GET: api/Contacts/ById
        [HttpGet("{id}")]
        public ActionResult<Contact?> GetContactById(Guid id)
        {
            return _crmService.contacts.GetContactById(id);
        }

        // GET: api/Contacts/ByEmail
        [HttpGet("GetContactByEmail")]
        public ActionResult<Contact?> GetContactByEmail(string email)
        {
            return _crmService.contacts.GetContactByEmail(email);
        }

        // GET: api/Contacts/ByPhone
        [HttpGet("GetContactByPhone")]
        public ActionResult<Contact?> GetContactByPhone(string phone)
        {
            Contact? c = _crmService.contacts.GetContactByPhone(phone);
            return c;
        }

        // PUT: api/Contacts
        [HttpPut("UpdateContact")]
        public IActionResult UpdateContact(ContactDTO contactdto)
        {
            Contact? c = _crmService.contacts.GetContactByEmail(contactdto.EmailAddress1);
            Contact? contact = _mapper.Map<Contact>(contactdto);
            contact.Email = c.Email;
            contact.ContactId = c.ContactId;
            contact = _crmService.contacts.UpdateContact(contact).Result;
            if (contact == null)
            {
                return BadRequest("Contact does not exist!");
            }
            return Ok("Contact updated sucessfully!");
        }

        // POST: api/Contacts
        [HttpPost]
        public ActionResult<Contact> InsertContact(ContactDTO contactdto)
        {
            Contact? contact = _mapper.Map<Contact>(contactdto);
            contact = _crmService.contacts.InsertContact(contact).Result;
            if (contact == null)
            {
                return BadRequest("Email already registered!");
            }
            return CreatedAtAction("GetContactById", new { id = contact.ContactId }, contact);
        }

        // DELETE: api/Contacts
        [HttpDelete("DeleteContact")]
        public IActionResult DeleteContact(Guid id)
        {
            Contact? contact = _crmService.contacts.GetContactById(id);
            if(contact == null)
            {
                return BadRequest("Contact does not exist!");
            }
            _ = _crmService.contacts.DeleteContact(contact).Result;
            return Ok("Contact deleted successfully!");
        }

    }
}
