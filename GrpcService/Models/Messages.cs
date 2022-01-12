using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcService.Models
{
    public class Messages
    {
        //[Key]
        public int MessagesId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? BinaryData { get; set; }
    }
}
