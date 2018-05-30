using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Areas.Admin.Dtos {
    public class PropertyDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}