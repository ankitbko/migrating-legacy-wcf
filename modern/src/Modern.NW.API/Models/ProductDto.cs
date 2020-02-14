using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Modern.NW.API.Models
{
    
    public class ProductDto
    {
        
        public int ProductID { get; set; }
        
        public string ProductName { get; set; }
        
        public Nullable<int> SupplierID { get; set; }
        
        public Nullable<int> CategoryID { get; set; }
        
        public string QuantityPerUnit { get; set; }
        
        public Nullable<decimal> UnitPrice { get; set; }
        
        public Nullable<short> UnitsInStock { get; set; }
        
        public Nullable<short> UnitsOnOrder { get; set; }
        
        public Nullable<short> ReorderLevel { get; set; }
        
        public bool Discontinued { get; set; }

        
        public virtual CategoryDto Category { get; set; }
        
        public virtual SupplierDto Supplier { get; set; }
    }
}
