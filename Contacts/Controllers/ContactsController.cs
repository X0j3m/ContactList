using Contacts.DTOs;
using Contacts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public ActionResult<ICollection<ContactDTO>> GetAll()
        {
            return Ok(_contactsService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ContactDTO?> GetById(Guid id)
        {
            var contact = _contactsService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public ActionResult CreateContact(CreateContactDTO createContactDto)
        {
            var contactId = _contactsService.Create(createContactDto);
            return CreatedAtAction(nameof(GetById), new { id = contactId }, contactId);
        }
    }
}
