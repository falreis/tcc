using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCC_Application
{
    public class DirectionsPoco
    {
        public TimeSpan Duration { get; set; }
        public TimeSpan DurationInTraffic { get; set; }
        public Double DistanceInMeters { get; set; }
        public TimeSpan ProcessTime { get; set; }

        public string STATUS_CODE { get; set; }
    }
}
