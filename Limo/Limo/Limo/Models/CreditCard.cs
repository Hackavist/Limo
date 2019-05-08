using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public class CreditCard : BaseModel
    {
        public string CardNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
