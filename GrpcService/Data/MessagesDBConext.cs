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

        private static List<Messages> GetMessages()
        {
            List<Messages> messages = new List<Messages>() {
              new Messages() {    // 1
               //MessagesId=1,
               Name = "Sound 1"
              },
              new Messages() {    // 2
               //MessagesId=2,
               Name = "Sound 2"
              },
              new Messages() {    // 3
               //MessagesId=3,
               Name = "Sound 3"
              },
              new Messages() {    // 4
               //MessagesId=4,
               Name = "Sound 4"
              },
              new Messages() {    // 5
               //MessagesId=5,
               Name = "Sound 5"
              },
            };

            return messages;
        }

    }
}
