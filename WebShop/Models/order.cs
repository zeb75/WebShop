using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual List<OrderRow> OrderRows { get; set; }

        public Order()
        {
            OrderRows = new List<OrderRow>();
        }
    }
}