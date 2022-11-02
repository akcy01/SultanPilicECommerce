using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.Models
{
    public class ChickType
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Ürün Tipi")]
        [Required]
        public string Name { get; set; }
    }
}
