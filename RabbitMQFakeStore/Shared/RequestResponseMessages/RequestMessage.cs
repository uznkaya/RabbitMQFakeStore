﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestResponseMessages
{
    public class RequestMessage
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public bool IsSucceeded { get; set; }
    }
}
