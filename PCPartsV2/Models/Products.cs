using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCPartsV2.Models
{
    public class Products
    {

        [Key, Required]
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public int Discount { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string Image { get; set; }

        public Suppliers Suppliers { get; set; }
        [ForeignKey("Suppliers"), Required]
        [Display(Name = "Supplier")]
        public int SupplierFK { get; set; }

        public ProductType ProductType { get; set; }
        [ForeignKey("ProductType"), Required]
        [Display(Name = "Product Type")]
        public int ProductTypeFK { get; set; }

}
}