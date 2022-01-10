using GrpcService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcService.Data
{
    public class MessagesDBConext: DbContext
    {

        public MessagesDBConext(DbContextOptions options) : base(options) {
            
        }

        public DbSet<Messages> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Messages>().HasData(new Messages { MessagesId = 1, Name = "http://sample.com" });
        }

 

    }
}
