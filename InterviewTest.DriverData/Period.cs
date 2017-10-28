using System;
using System.Diagnostics;

namespace InterviewTest.DriverData
{
	[DebuggerDisplay("{_DebuggerDisplay,nq}")]
	public class Period
	{
        // BONUS: What's the difference between DateTime and DateTimeOffset?
        //DateTime is TimeZone dependant.
        //DateTimeOffset is a moment in time that is universal for everyone, and is independant of TimeZone.
        public DateTimeOffset Start;
		public DateTimeOffset End;

        // BONUS: What's the difference between decimal and double?
        //double is faster than decimal, but decimal is accurate than double.
        public decimal AverageSpeed;

		private string _DebuggerDisplay => $"{Start:t} - {End:t}: {AverageSpeed}";
	}
}
