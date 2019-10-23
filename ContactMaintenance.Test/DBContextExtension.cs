using System;
using System.Collections.Generic;
using System.Text;

namespace ContactMaintenance.Test
{
    public static class DBContextExtension
    {
        public static void Seed(this ContactContext dbContext)
        {
            dbContext.Contacts.Add(new Models.Contacts()
            {
                FirstName="Anupam",
                LastName="Singh",
                Email="Anupamsingh@gmail",
                PhoneNumber="131-123-1233",
                Status=Models.ContactStatus.Active
            });
            dbContext.Contacts.Add(new Models.Contacts()
            {
                FirstName = "Rohit",
                LastName = "Singh",
                Email = "rohitsingh@gmail",
                PhoneNumber = "131-876-1233",
                Status = Models.ContactStatus.Active
            });
            dbContext.Contacts.Add(new Models.Contacts()
            {
                FirstName = "Ram",
                LastName = "Kumar",
                Email = "ramkumar@gmail",
                PhoneNumber = "131-123-1233",
                Status = Models.ContactStatus.Active
            });
            dbContext.SaveChanges();
        }
    }
}
