using InterviewTest.DriverData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class DeliveryDriverAnalyser : IAnalyser
	{
        private AnalyserSettings AnalyserSettings { get; set; }
        public DeliveryDriverAnalyser(AnalyserSettings analyserSettings) {
            AnalyserSettings = analyserSettings;
        }
		public HistoryAnalysis Analyse(IReadOnlyCollection<Period> history)
		{
            HistoryAnalysis result = new HistoryAnalysis { AnalysedDuration = new TimeSpan(0, 0, 0), DriverRating = 0 };
            if (history != null && history.Count > 0)
            {
                //Group the history data for each day.
                var days = history.GroupBy(x => x.Start.Date);
                var periodRatings = new List<PeriodRating>();
                var analysedDuration = new TimeSpan();
                
                //Calculate rating for each day
                foreach (var day in days)
                {
                    //Get all periods which fall under the permitted time of the day.
                    var validPeriods = AnalyserHelpers.GetValidPeriods(day.ToList(), AnalyserSettings);
                    var undocumentPeriods = AnalyserHelpers.GetUnDocumentedPeriodWithRating(validPeriods, AnalyserSettings);

                    //Calculate rating for each valid period in a day
                    if (validPeriods != null && validPeriods.Any())
                    {
                        periodRatings.AddRange(AnalyserHelpers.CalculateRatingForValidPeriods(validPeriods, AnalyserSettings, out analysedDuration));
                    }

                    //Calculate rating for each undocumented period in a day
                    if (undocumentPeriods != null && undocumentPeriods.Any())
                    {
                        periodRatings.AddRange(AnalyserHelpers.CalculateRatingForUndocumentedPeriods(undocumentPeriods));
                    }
                }

                if (periodRatings != null && periodRatings.Any())
                {
                    result = new HistoryAnalysis();
                    result.DriverRating = AnalyserHelpers.CalculateOverallWeightedRating(periodRatings, AnalyserSettings);
                    result.AnalysedDuration = analysedDuration;
                }
            }
            return result;
        }
	}
}