using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Akelote_e_Shop.Models
{
    [DataContract]
    public class Payment
    {
        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        [Required]
        public string Number { get; set; }

        [DataMember]
        [Required]
        public string Holder { get; set; }

        [DataMember(Name = "exp_year")]
        public int ExpiryYear { get; set; }

        [DataMember(Name = "exp_month")]
        public int ExpiryMonth { get; set; }

        [DataMember]
        [Required]
        public string Cvv { get; set; }
    }
}