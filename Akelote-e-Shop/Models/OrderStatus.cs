using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akelote_e_Shop.Models
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled,
        Refunded,
        Shipping
    }
}