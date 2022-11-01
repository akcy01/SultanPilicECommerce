using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sultan.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Kategori Adı")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime? CreatedDateTime { get; set; } = DateTime.Now;
    }
}
