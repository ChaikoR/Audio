using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcService.Models
{
    public class Messages
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MessagesId { get; set; }
        public string? Name { get; set; }
    }
}
