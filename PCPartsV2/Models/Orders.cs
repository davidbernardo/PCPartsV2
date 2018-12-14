using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCPartsV2.Models
{
    public class Orders
    {
        [Key, Required]
        public int OrderID { get; set; }

        [Required]
        [Display(Name ="Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


    }
}