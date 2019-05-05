using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public class Car : BaseModel
    {
        public string color { get; set; }
        public string Model { get; set; }
        public DateTime ProducationDate { get; set; }
    }
}
