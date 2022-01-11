using System.ComponentModel.DataAnnotations;

namespace Blazor.Shared.Models
{
    public class Messages
    {
        [Key]
        public int MessagesId { get; set; }
        [Required]
        [StringLength(125, ErrorMessage = "Длинное название.")]
        public string Name { get; set; }=String.Empty;

        public byte[]? BinaryData { get; set; }
        
    }
}
