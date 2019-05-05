using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public class Request : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}
