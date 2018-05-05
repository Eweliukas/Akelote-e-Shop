using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Akelote_e_Shop.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        [Required]
        [DisplayName("Order price")]
        public int OrderPrice { get; set; }

        public int Quantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}