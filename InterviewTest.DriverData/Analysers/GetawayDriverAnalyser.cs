using InterviewTest.DriverData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class GetawayDriverAnalyser : IAnalyser
	{
        private AnalyserSettings AnalyserSettings { get; set; }
        public GetawayDriverAnalyser(AnalyserSettings analyserSettings)
        {
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
                var isUndocumentedPeriodAvailable = false;

                //Calculate rating for each day
                foreach (var day in days)
                {
                    var periods = day.ToList();
                    //Remove all zero speed periods from start and end
                    while (periods.First().AverageSpeed <= 0 || periods.Last().AverageSpeed <= 0)
                    {
                        //If the first period has 0 speed remove it.
                        if (periods.First().AverageSpeed <= 0)
                        {
                            periods.Remove(periods.First());
                        }
                        //If the last period has 0 speed remove it.
                        if (periods.Last().AverageSpeed <= 0)
                        {
                            periods.Remove(periods.Last());
                        }
                    }

                    //Get all periods order by start time.
                    var validPeriods = AnalyserHelpers.GetValidPeriods(periods, AnalyserSettings);
                    if (validPeriods != null && validPeriods.Any())
                    {
                        var undocumentPeriods = AnalyserHelpers.GetUnDocumentedPeriodWithRating(validPeriods, AnalyserSettings);

                        //Calculate rating for each valid period in a day
                        if (validPeriods != null && validPeriods.Any())
                        {
                            periodRatings.AddRange(AnalyserHelpers.CalculateRatingForValidPeriods(validPeriods, AnalyserSettings, out analysedDuration));
                        }
                        //Calculate rating for each undocumented period in a day
                        if (undocumentPeriods != null && undocumentPeriods.Any())
                        {
                            isUndocumentedPeriodAvailable = true;
                            periodRatings.AddRange(AnalyserHelpers.CalculateRatingForUndocumentedPeriods(undocumentPeriods));
                        }
                    }
                }
                if (periodRatings != null && periodRatings.Any())
                {
                    result = new HistoryAnalysis();
                    result.DriverRating = AnalyserHelpers.CalculateOverallWeightedRating(periodRatings, AnalyserSettings, isUndocumentedPeriodAvailable);
                    result.AnalysedDuration = analysedDuration;
                }
            }
            return result;
        }
	}
}