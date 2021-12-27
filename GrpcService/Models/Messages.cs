using System.ComponentModel.DataAnnotations;

namespace GrpcService.Models
{
    public class Messages
    {
        [Key]
        public int MessagesId { get; set; }
        public string? Name { get; set; }
    }
}
