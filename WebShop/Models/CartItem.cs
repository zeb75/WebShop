using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductRefId { get; set; }

        public Product Product { get; set; }

        public int Amount { get; set; }
    }
}