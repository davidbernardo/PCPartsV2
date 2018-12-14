using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCPartsV2.Models
{
    public class ProductType
    {
        [Key, Required]
        [Display(Name = "Product Type")]
        public int ProductTypeID { get; set; }

        [Required]
        public string Type { get; set; }
        
    }
}