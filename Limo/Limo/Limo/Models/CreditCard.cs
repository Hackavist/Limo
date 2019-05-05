using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public class CreditCard : BaseModel
    {
        public int CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
