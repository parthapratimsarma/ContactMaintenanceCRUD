using ContactMaintenance.Controllers;
using ContactMaintenance.Provider;
using ContactMaintenance.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactMaintenance.Test
{
    public class CaontactControllerTest
    {
        private ContactsController controller;
        private ContactContext dbContext;

        public CaontactControllerTest()
        {
            dbContext = MockDBContext.GetMockContext("DBContacts");
            IContactRepository repository = new ContactRepository(dbContext);
           // ILogger<ContactsController> logger = new DBLoggerProvider().CreateLogger()
            controller = new ContactsController(repository);
        }

        [Fact]
        public void TestGetContacts()
        {
            // Act
            var response = controller.GetContacts();
            var contacts = response;

            dbContext.Dispose();

            // Assert
            Assert.True(contacts != null);
        }

        [Fact]
        public void TestGetContactById()
        {
            // Act
            var response = controller.GetContacts(1);
            var contact = response;

            dbContext.Dispose();
            // Assert
            Assert.True(contact != null);
        }

        [Fact]
        public void TestInsertContact()
        {
            // Act
            var response = controller.PostContacts(new Models.Contacts()
            {
                FirstName = "Arif",
                LastName = "Ali",
                Email = "ariali@gmail",
                PhoneNumber = "131-123-1233",
                Status = "Active"
            });

            var contact = response.Result;

            dbContext.Dispose();
            // Assert
            Assert.True(contact != null);
        }

        [Fact]
        public void UpdateContact()
        {
            var response = controller.GetContacts();
            var contactToUpdate = response.ToList().Where(x => x.ContactId == 1).FirstOrDefault();
            var updateResponse = controller.PutContacts(contactToUpdate.ContactId, contactToUpdate);
            dbContext.Dispose();
            // Assert
            Assert.True(updateResponse != null);


        }
    }
}
