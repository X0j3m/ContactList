using Contacts.DTOs;
using Contacts.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<ContactDTO> GetById(Guid id)
        {
            var contact = _contactsService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateContact(CreateContactDTO createContactDto)
        {
            var contactId = _contactsService.Create(createContactDto);
            if(contactId == Guid.Empty)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = contactId }, contactId);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public ActionResult UpdateContact(Guid id, UpdateContactDTO updateContactDto)
        {
            if (GetById(id).Result.GetType() == typeof(NotFoundResult))
            {
                return NotFound();
            }
            var updatedContactId = _contactsService.Update(id, updateContactDto);
            if (updatedContactId == Guid.Empty)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public ActionResult DeleteContact(Guid id)
        {
            var success = _contactsService.Delete(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
