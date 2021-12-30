using System.ComponentModel.DataAnnotations;

namespace Blazor.Shared.Models
{
    public class Messages
    {
        [Key]
        public int MessagesId { get; set; }
        public string? Name { get; set; }
        //public byte[]? BinaryData { get; set; }
    }
}
