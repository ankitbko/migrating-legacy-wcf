using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Modern.NW.API.Models
{
    
    public class OrderDetailDto
    {
        
        public int OrderID { get; set; }
        
        public int ProductID { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public short Quantity { get; set; }
        
        public float Discount { get; set; }

        
        public virtual ProductDto Product { get; set; }
    }
}
