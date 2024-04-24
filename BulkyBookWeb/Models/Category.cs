using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Display Order must be between 1 and 100 only")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public Category()
        {
            CreatedDateTime = DateTime.Now;
        }

    }
}
