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

        /// <summary>
        /// Get all contacts. "api/Contacts"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Contacts> GetContacts()
        {
            try
            {
                var contactList = _repository.GetAllContacts();
                _logger.LogInformation("Contact List Retrieved");
                return contactList;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException!=null?ex.InnerException.Message:ex.StackTrace);
                throw ex;
            }
        }

        
        /// <summary>
        /// Get contacts by contact Id. "api/Contacts/5"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContacts([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var contacts = await _repository.GetContactById(id);
                _logger.LogInformation($"Contact List Retrieved for contact id {contacts.ContactId}");

                if (contacts == null)
                {
                    return NotFound();
                }

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException!=null?ex.InnerException.Message:ex.StackTrace);
                throw ex;
            }
        }


        /// <summary>
        /// Update contact by contact id. "api/Contacts/5"
        /// </summary>
        /// <param name="id"> contact id</param>
        /// <param name="contacts"> contact model to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult PutContacts([FromRoute] long id, [FromBody] Contacts contacts)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException!=null?ex.InnerException.Message:ex.StackTrace);
                throw ex;
            }

            return NoContent();
        }


        /// <summary>
        /// Add contact to DB. "api/Contacts"
        /// </summary>
        /// <param name="contacts">Contact model to add to DB</param>
        /// <returns></returns>      
        [HttpPost]
        public IActionResult PostContacts([FromBody] Contacts contacts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _repository.InsertContact(contacts);
                return CreatedAtAction("GetContacts", new { id = contacts.ContactId }, contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException!=null?ex.InnerException.Message:ex.StackTrace);
                throw ex;
            }

        }


        /// <summary>
        /// Delete contact rom DB by contact Id. "api/Contacts/5"
        /// </summary>
        /// <param name="id">contact id to delete rom DB</param>
        /// <returns></returns>
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
            try
            {
                _repository.DeleteCotact(id);

                return Ok(contacts);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException!=null?ex.InnerException.Message:ex.StackTrace);
                throw ex;
            }

        }

/// <summary>
/// Check if contact exists or not
/// </summary>
/// <param name="id"> contact id</param>
/// <returns></returns>
private bool ContactsExists(long id)
        {
            return _repository.GetAllContacts().Any(e => e.ContactId == id);
        }
    }
}