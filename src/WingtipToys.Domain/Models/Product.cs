using System;
using System.Collections.Generic;

namespace WingtipToys.Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double? UnitPrice { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
