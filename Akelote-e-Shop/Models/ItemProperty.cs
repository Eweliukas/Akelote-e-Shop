using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class ItemProperty
    {    
        public int ItemPropertyId { get; set; }
        public int ItemId { get; set; }
        public int PropertyId { get; set; }       
        [Required]
        public string PropertyValue { get; set; }

        public virtual Item Item { get; set; }
        public virtual Property Property { get; set; }
        
    }
}