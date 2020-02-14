using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GH.Northwind.Business.Interfaces.DTO
{
    [DataContract]
    public class CategoryDto
    {
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public byte[] Picture { get; set; }
    }
}
