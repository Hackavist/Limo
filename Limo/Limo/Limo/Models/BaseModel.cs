using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; } = 0;
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
