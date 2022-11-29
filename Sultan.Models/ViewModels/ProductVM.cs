using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.Models.ViewModels
{
    public class ProductVM
    {
        /* Aşağıda tanımladığımız IEnumerable'lar ile ilişkisel bir yapı kurduk.Kategori ve tavuk tipi ile ürün arasında. */
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ChickTypeList { get; set; }

    }
}
