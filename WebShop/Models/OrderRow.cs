using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class OrderRow
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }

        public virtual Product Product { get; set; }
    }
}