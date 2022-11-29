using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultan.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public double ListPrice { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public int ChickTypeId { get; set; }
        [ValidateNever]
        public ChickType ChickType { get; set; }


    }
}
