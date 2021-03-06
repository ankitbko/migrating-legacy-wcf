//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace GH.Northwind.Business.Entities
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Order))]
    [KnownType(typeof(Product))]
    public partial class Order_Detail
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
        public virtual Order Order { get; set; }
        [DataMember]
        public virtual Product Product { get; set; }
    }
    
}
