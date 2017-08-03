﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Orbital7.Extensions.Attributes
{
    public class PhoneStringLengthAttribute : StringLengthAttribute
    {
        public PhoneStringLengthAttribute()
            : base(20)
        {

        }
    }
}
