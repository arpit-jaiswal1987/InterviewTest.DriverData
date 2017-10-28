using InterviewTest.DriverData.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData.Analysers
{
	// BONUS: Why internal?
	internal class FormulaOneAnalyser : IAnalyser
	{
        private AnalyserSettings AnalyserSettings { get; set; }
        public FormulaOneAnalyser(AnalyserSettings analyserSettings)
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
                
                //Calculate rating for each day
                foreach (var day in days)
                {
                    //Get all periods order by start time.
                    var validPeriods = AnalyserHelpers.GetValidPeriods(day.ToList(), AnalyserSettings);

                    //Remove all zero speed periods from start and end
                    while (validPeriods.First().AverageSpeed <= 0 || validPeriods.Last().AverageSpeed <= 0)
                    {
                        //If the first period has 0 speed remove it.
                        if (validPeriods.First().AverageSpeed <= 0)
                        {
                            validPeriods.Remove(validPeriods.First());
                        }
                        //If the last period has 0 speed remove it.
                        if (validPeriods.Last().AverageSpeed <= 0)
                        {
                            validPeriods.Remove(validPeriods.Last());
                        }
                    }
                    if (validPeriods != null && validPeriods.Any())
                    {
                        var analyserSettingCopy = new AnalyserSettings() { SpeedLimit = AnalyserSettings.SpeedLimit };
                        analyserSettingCopy.StartOfDay = new TimeSpan(validPeriods.First().Start.Hour, validPeriods.First().Start.Minute, validPeriods.First().Start.Second);
                        analyserSettingCopy.EndOfDay = new TimeSpan(validPeriods.Last().End.Hour, validPeriods.Last().End.Minute, validPeriods.Last().End.Second);

                        var undocumentPeriods = AnalyserHelpers.GetUnDocumentedPeriodWithRating(validPeriods, analyserSettingCopy);

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