using ContactMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance
{
    public class ContactContext:DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        {
        }

        public DbSet<Contacts> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contacts>().HasIndex(x => x.Email).IsUnique();
            builder
        .Entity<Contacts>()
        .Property(e => e.Status)
        .HasConversion(
            v => v.ToString(),
            v => (ContactStatus)Enum.Parse(typeof(ContactStatus), v));
        }
    }
}
