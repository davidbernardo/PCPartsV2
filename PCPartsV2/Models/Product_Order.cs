using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCPartsV2.Models
{
    public class Product_Order
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Orders Orders { get; set; }
        [ForeignKey("Orders"), Required]
        public int OrderFK { get; set; }

        public Products Products { get; set; }
        [ForeignKey("Products"), Required]
        public int ProductFK { get; set; }
    }
}