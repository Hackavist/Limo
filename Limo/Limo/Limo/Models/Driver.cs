﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Limo.Models
{
    public class Driver : BaseModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public bool IsAvailable { get; set; }
    }
}