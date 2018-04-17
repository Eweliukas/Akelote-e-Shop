using System.Runtime.Serialization;

namespace Akelote_e_Shop.Models
{
    [DataContract]
    public class Payment
    {
        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Holder { get; set; }

        [DataMember(Name = "exp_year")]
        public int ExpiryYear { get; set; }

        [DataMember(Name = "exp_month")]
        public int ExpiryMonth { get; set; }

        [DataMember]
        public string Cvv { get; set; }
    }
}