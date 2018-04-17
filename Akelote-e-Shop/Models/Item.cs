﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public int Price { get; set; }
        public string Description { get; set; }
        public int? Discount { get; set; }

        public virtual ICollection<ItemProperty> ItemProperties { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public string ReadablePrice
        {
            get
            {
                return PriceToReadable(Price);
            }
        }

        public static string PriceToReadable(int price)
        {
            return (price / 100) + "," + (price % 100).ToString().PadLeft(2, '0') + " €";
        }
    }
}