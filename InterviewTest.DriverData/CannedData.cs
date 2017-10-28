using System;
using System.Collections.Generic;

namespace InterviewTest.DriverData
{
	public static class CannedDrivingData
	{
		private static readonly DateTimeOffset _day = new DateTimeOffset(2016, 10, 13, 0, 0, 0, 0, TimeSpan.Zero);

		// BONUS: What's so great about IReadOnlyCollections?
		public static readonly IReadOnlyCollection<Period> History = new[]
		{
			new Period
			{
				Start = _day + new TimeSpan(0, 0, 0),
				End = _day + new TimeSpan(8, 54, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(8, 54, 0),
				End = _day + new TimeSpan(9, 28, 0),
				AverageSpeed = 28m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 28, 0),
				End = _day + new TimeSpan(9, 35, 0),
				AverageSpeed = 33m
			},
			new Period
			{
				Start = _day + new TimeSpan(9, 50, 0),
				End = _day + new TimeSpan(12, 35, 0),
				AverageSpeed = 25m
			},
			new Period
			{
				Start = _day + new TimeSpan(12, 35, 0),
				End = _day + new TimeSpan(13, 30, 0),
				AverageSpeed = 0m
			},
			new Period
			{
				Start = _day + new TimeSpan(13, 30, 0),
				End = _day + new TimeSpan(19, 12, 0),
				AverageSpeed = 29m
			},
			new Period
			{
				Start = _day + new TimeSpan(19, 12, 0),
				End = _day + new TimeSpan(24, 0, 0),
				AverageSpeed = 0m
			}
		};
        public static readonly IReadOnlyCollection<Period> EmptyHistory = new Period[]{ };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverOutOfPermittedTime = new[] {
            new Period { Start=_day+new TimeSpan(8,0,0),
                End=_day+new TimeSpan(8,59,59),
                AverageSpeed=25m },
            new Period {
            Start=_day+new TimeSpan(17,0,1),
            End=_day+new TimeSpan(17,10,0),
            AverageSpeed=25m}
        };

        public static readonly IReadOnlyCollection<Period> DeliveryDriverExceedSpeedLimit = new[] {
            new Period { Start=_day+new TimeSpan(9,0,0),
                End=_day+new TimeSpan(9,59,59),
                AverageSpeed=30.1m },
        };
        public static readonly IReadOnlyCollection<Period> FormulaOneDriverExceedSpeedLimit = new[]
        {
            new Period {
                Start =_day+new TimeSpan(0,0,0),
                End=_day+new TimeSpan(1,0,0),
                AverageSpeed=201
            }
        };
        public static readonly IReadOnlyCollection<Period> FormulaOneDriverExceedSpeedLimitWithPenalty = new[]
        {
            new Period {
                Start =_day+new TimeSpan(1,0,0),
                End=_day+new TimeSpan(2,0,0),
                AverageSpeed=201
            },
             new Period {
                Start =_day+new TimeSpan(2,0,1),
                End=_day+new TimeSpan(3,0,1),
                AverageSpeed=201
            }
        };
    }
}
