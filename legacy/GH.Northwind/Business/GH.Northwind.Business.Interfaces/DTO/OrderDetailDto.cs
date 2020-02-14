using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GH.Northwind.Business.Interfaces.DTO
{
    [DataContract]
    public class OrderDetailDto
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public decimal UnitPrice { get; set; }
        [DataMember]
        public short Quantity { get; set; }
        [DataMember]
        public float Discount { get; set; }

        [DataMember]
        public virtual ProductDto Product { get; set; }
    }
}
