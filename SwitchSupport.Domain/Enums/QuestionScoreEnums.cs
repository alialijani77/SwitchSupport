﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwitchSupport.Domain.Enums
{
    public enum QustionScore
    {
        [Display(Name = "مثبت")] Plus,
        [Display(Name = "منفی")] Minus
    }
}
