﻿using System.Collections.Generic;
using System.Linq;

namespace CA.Algorithms.Data.MedianMaintenance
{
    public class GetMedianMaintenanceDataTest : IGetMedianMaintenanceData
    {
        public IEnumerable<int> GetData()
        {
            return new[] {2, 4, 6, 3, 5, 2, 7, 9}.AsEnumerable();
        }
    }
}
