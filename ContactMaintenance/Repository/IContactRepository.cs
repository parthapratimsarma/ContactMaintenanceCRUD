using ContactMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance.Repository
{
    public interface IContactRepository
    {
        List<Contacts> GetAllContacts();
        Task<Contacts> GetContactById(long contactId);
        void InsertContact(Contacts contact);
        void UpdateContact(Contacts contact);
        void DeleteCotact(long id);
        void SaveChanges();
    }
}
