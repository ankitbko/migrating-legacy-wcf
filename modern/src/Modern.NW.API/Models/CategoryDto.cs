using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Modern.NW.API.Models
{
    
    public class CategoryDto
    {
        
        public int CategoryID { get; set; }
        
        public string CategoryName { get; set; }
        
        public string Description { get; set; }
        
        public byte[] Picture { get; set; }
    }
}
