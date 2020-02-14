using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GH.Northwind.Business.Interfaces.DTO
{
    [DataContract]
    public class OrderDto
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public Nullable<int> EmployeeID { get; set; }
        [DataMember]
        public Nullable<System.DateTime> OrderDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> RequiredDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ShippedDate { get; set; }
        [DataMember]
        public Nullable<int> ShipVia { get; set; }
        [DataMember]
        public Nullable<decimal> Freight { get; set; }
        [DataMember]
        public string ShipName { get; set; }
        [DataMember]
        public string ShipAddress { get; set; }
        [DataMember]
        public string ShipCity { get; set; }
        [DataMember]
        public string ShipRegion { get; set; }
        [DataMember]
        public string ShipPostalCode { get; set; }
        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public virtual CustomerDto Customer { get; set; }
        [DataMember]
        public virtual ICollection<OrderDetailDto> Order_Details { get; set; }
    }
}
