using SmsService2.Models;
using SmsService2.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsService2.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
    }
}
