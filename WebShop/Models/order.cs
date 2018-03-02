using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public int Id { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public virtual List<OrderRow> OrderRows { get; set; }

        public Order()
        {
            OrderRows = new List<OrderRow>();
        }
    }
}