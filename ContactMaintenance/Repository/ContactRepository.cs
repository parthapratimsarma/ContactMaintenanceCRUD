using ContactMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance.Repository
{
    public class ContactRepository : IContactRepository
    {
        private ContactContext _contactContext;

        public ContactRepository(ContactContext context)
        {
            _contactContext = context;
        }
        public void DeleteCotact(long id)
        {
            _contactContext.Contacts.Remove(_contactContext.Contacts.Find(id));
            SaveChanges();
        }

        public List<Contacts> GetAllContacts()
        {
            try
            {
                return _contactContext.Contacts.ToList();
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }

        public void InsertContact(Contacts contact)
        {
            try
            {
                _contactContext.Contacts.Add(contact);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveChanges()
        {
            try
            {
                _contactContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateContact(Contacts contact)
        {
            _contactContext.Entry(contact).State = EntityState.Modified;
            SaveChanges();
        }

        public async Task<Contacts> GetContactById(long contactId)
        {
            try
            {
                return await _contactContext.Contacts.FindAsync(contactId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
