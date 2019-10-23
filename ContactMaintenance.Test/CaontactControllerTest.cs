using ContactMaintenance.Controllers;
using ContactMaintenance.Provider;
using ContactMaintenance.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //_logger = logger;
            dbContext = MockDBContext.GetMockContext("DBContacts");
            var serviceProvider = new ServiceCollection()
                                    .AddLogging()
                                    .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<ContactsController>();
            IContactRepository repository = new ContactRepository(dbContext);
            controller = new ContactsController(repository, logger);

        }

        [Fact]
        public void TestGetContacts()
        {

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
                Status = Models.ContactStatus.Active
            });

            var contact = response;

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
