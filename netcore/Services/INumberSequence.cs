﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Services
{
    public interface INumberSequence
    {
        string GetNumberSequence(string module);
    }
}