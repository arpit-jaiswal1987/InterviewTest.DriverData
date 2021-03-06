﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData
{
    public class AnalyserSettings
    {
        public TimeSpan StartOfDay { get; set; }
        public TimeSpan EndOfDay { get; set; }
        public decimal SpeedLimit { get; set; }
        public decimal ExceedSpeedLimitRating { get; set; }
        public bool ApplyUnDocumentedPenaltyFlag { get; set; }
        public decimal UndocumentedPenalty { get; set; }
    }
}
