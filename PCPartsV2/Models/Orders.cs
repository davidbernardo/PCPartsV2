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
        /*public Orders()
        {
            ListProductOrder = new HashSet<Product_Order>();
        }*/

        [Key, Required]
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        //public virtual ICollection<Product_Order> ListProductOrder { get; set; }

    }
}