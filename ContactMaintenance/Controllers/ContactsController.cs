using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactMaintenance;
using ContactMaintenance.Models;
using Microsoft.AspNetCore.Cors;
using ContactMaintenance.Repository;
using Microsoft.Extensions.Logging;

namespace ContactMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ContactsController : ControllerBase
    {
        //private readonly ContactContext _context;
        private IContactRepository _repository;
        private ILogger<ContactsController> _logger;

        public ContactsController(IContactRepository repository, ILogger<ContactsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<Contacts> GetContacts()
        {
            var contactList = _repository.GetAllContacts();
            _logger.LogInformation("Contact List Retrieved");
            return contactList;
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacts([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacts = await _repository.GetContactById(id);
            _logger.LogInformation($"Contact List Retrieved for contact id {contacts.ContactId}");

            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacts([FromRoute] long id, [FromBody] Contacts contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacts.ContactId)
            {
                return BadRequest();
            }

            try
            {
                _repository.UpdateContact(contacts);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]        
        public async Task<IActionResult> PostContacts([FromBody] Contacts contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.InsertContact(contacts);

            return CreatedAtAction("GetContacts", new { id = contacts.ContactId }, contacts);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacts([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contacts = await _repository.GetContactById(id);
            if (contacts == null)
            {
                return NotFound();
            }

            _repository.DeleteCotact(id);

            return Ok(contacts);
        }

        private bool ContactsExists(long id)
        {
            return _repository.GetAllContacts().Any(e => e.ContactId == id);
        }
    }
}