using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Models
{
    public class ReviewReply
    {
        [Key] public int Id { get; set; }
        [Required] public int ReviewId { get; set; }
        [Required, StringLength(1000)] public string Reply { get; set; } = string.Empty;
        [Required] public string RepliedBy { get; set; } = string.Empty;
        public DateTime RepliedAt { get; set; } = DateTime.UtcNow;

        public Review? Review { get; set; }
    }

    // Tag.cs & MenuItemTag.cs
    public class Tag
    {
        [Key] public int Id { get; set; }
        [Required, StringLength(50)] public string Name { get; set; } = string.Empty;
        [StringLength(200)] public string? Description { get; set; }
        public ICollection<MenuItemTag>? MenuItemTags { get; set; }
    }

    public class MenuItemTag
    {
        [Key] public int Id { get; set; }
        [Required] public int MenuItemId { get; set; }
        [Required] public int TagId { get; set; }

        public MenuItem? MenuItem { get; set; }
        public Tag? Tag { get; set; }
    }
}
