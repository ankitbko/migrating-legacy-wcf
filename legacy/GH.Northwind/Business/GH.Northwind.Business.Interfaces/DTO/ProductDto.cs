using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GH.Northwind.Business.Interfaces.DTO
{
    [DataContract]
    public class ProductDto
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public Nullable<int> SupplierID { get; set; }
        [DataMember]
        public Nullable<int> CategoryID { get; set; }
        [DataMember]
        public string QuantityPerUnit { get; set; }
        [DataMember]
        public Nullable<decimal> UnitPrice { get; set; }
        [DataMember]
        public Nullable<short> UnitsInStock { get; set; }
        [DataMember]
        public Nullable<short> UnitsOnOrder { get; set; }
        [DataMember]
        public Nullable<short> ReorderLevel { get; set; }
        [DataMember]
        public bool Discontinued { get; set; }

        [DataMember]
        public virtual CategoryDto Category { get; set; }
        [DataMember]
        public virtual SupplierDto Supplier { get; set; }
    }
}
