using ContactMaintenance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance
{
    public class LogContext:DbContext
    {
        public static string ConnectionString { get; set; }
        public LogContext(DbContextOptions<LogContext> options)
           : base(options)
        {
        }

        public DbSet<LogRecord> LogRecord { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConnectionString);
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
