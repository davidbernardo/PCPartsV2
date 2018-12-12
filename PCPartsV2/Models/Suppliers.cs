using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PCPartsV2.Models
{
    public class Suppliers
    {
        /*public Suppliers()
        {
            ListProducts = new HashSet<Products>();
        }*/

        [Key, Required]
        public int SupplierID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        //public virtual ICollection<Products> ListProducts { get; set; }
    }
}