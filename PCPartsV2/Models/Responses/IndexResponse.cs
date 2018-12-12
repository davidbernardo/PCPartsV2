using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCPartsV2.Models.Responses
{
    public class IndexResponse
    {
        public List<Products> RecentProducts { get; set; }
        public List<Products> DiscountedProducts { get; set; }
    }
}