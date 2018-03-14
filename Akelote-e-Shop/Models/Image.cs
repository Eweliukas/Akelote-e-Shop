using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [Required]
        public string HyperLink { get; set; }
        public string Caption { get; set; }
    }
}