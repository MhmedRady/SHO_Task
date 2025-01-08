﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHO_Task.Domain.BuildingBlocks
{
    public interface ISHONumberGenerator
    {
        string GenerateSHONumber(DateTime createdDate);
    }
}
