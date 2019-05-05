using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int NationalId { get; set; }
        public int CreditCardId { get; set; }
        public virtual CreditCard CrieditCard { get; set; }
    }
}
