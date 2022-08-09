using Microsoft.AspNetCore.Mvc;
using CRMServer.Models.CRM;
using CRMClient;
using AutoMapper;
using CRMServer.DTO;
using CRMClient.contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            _mapper = mapper;
            _crmService = crmService;
        }
        // GET: api/Contacts
        [HttpGet]
        /*[Authorize(Roles = "Admin")]*/
        public IEnumerable<Contact> GetContacts()
        {
            return _crmService.contacts.GetAllContacts();
        }

        // GET: api/Contacts/ById
        [HttpGet("Primary/{id}")]
        [Authorize(Roles = "Primary, Admin")]
        public ActionResult<Contact?> GetContactByIdPrimary(Guid id)
        {
            return _crmService.contacts.GetContactById(id);
        }
        // GET: api/Contacts/ById
        [HttpGet("User/{id}")]
        [Authorize(Roles = "User")]
        public ActionResult<Contact?> GetContactByIdUser(Guid id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid? contactId = _crmService.contacts.GetContactByEmail(userEmail)?.ContactId;
            if(contactId == id)
            {
                return _crmService.contacts.GetContactById(id);
            }
            return NotFound();
        }

        // GET: api/Contacts/ByEmail
        [HttpGet("GetContactByEmailPrimary")]
        [Authorize(Roles = "Primary, Admin")]
        public ActionResult<Contact?> GetContactByEmailPrimary(string email)
        {
            return _crmService.contacts.GetContactByEmail(email);
        }
        // GET: api/Contacts/ByEmail
        [HttpGet("GetContactByEmail")]
        [Authorize(Roles = "User, Primary, Admin")]
        public ActionResult<Contact?> GetContactByEmail(string email)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userEmail == email)
            {
                return _crmService.contacts.GetContactByEmail(email);
            }
            return NotFound();
        }

        // GET: api/Contacts/ByPhone
        [HttpGet("GetContactByPhone")]
        [Authorize(Roles = "Primary, Admin")]
        public ActionResult<Contact?> GetContactByPhonePrimary(string phone)
        {
            Contact? c = _crmService.contacts.GetContactByPhone(phone);
            return c;
        }

        // PUT: api/Contacts
        [HttpPut("UpdateContact")]
        [Authorize(Roles = "Admin, Primary, User")]
        public IActionResult UpdateContact(ContactDTO contactdto)
        {
            Contact? contact = _mapper.Map<Contact>(contactdto);
            if (IsCurrentUser()?.ContactId == contact.ContactId)
            {
                _ = _crmService.contacts.UpdateContact(contact).Result;
                return Ok("Contact updated sucessfully!");
            }
            return Unauthorized();

        }

        // POST: api/Contacts
        [HttpPost]
        [Authorize(Roles = "Primary, Admin")]
        public ActionResult<Contact?> InsertContact(ContactDTO contactdto)
        {
            Contact? contact = _mapper.Map<Contact>(contactdto);
            if (contact == null)
            {
                return BadRequest("Email already registered!");
            }
            contact = _crmService.contacts.InsertContact(contact).Result;
            return CreatedAtAction("GetContactById", new { id = contact?.ContactId }, contact);
        }

        // DELETE: api/Contacts
        [HttpDelete("DeleteContact")]
        [Authorize(Roles = "Primary, Admin")]
        public IActionResult DeleteContact(Guid id)
        {
            Contact? contact = _crmService.contacts.GetContactById(id);
            if(contact == null)
            {
                return NotFound("Contact does not exist!");
            }
            _ = _crmService.contacts.DeleteContact(contact).Result;
            return Ok("Contact deleted successfully!");
        }

        //Current User
        private Contact? IsCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _crmService.contacts.GetContactByEmail(userEmail);
        }

    }
}
