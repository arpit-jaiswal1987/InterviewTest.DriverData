using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class AnalyserHelpers
    {
        /// <summary>
        /// Gets the valid periods as per the permitted time.
        /// </summary>
        /// <param name="periods"></param>
        /// <param name="analyserSettings"></param>
        /// <returns></returns>
        public static List<Period> GetValidPeriods(List<Period> periods, AnalyserSettings analyserSettings)
        {
            //If the Start and End time for analyser is set.
            if (periods != null && periods.Any() && analyserSettings != null && analyserSettings.StartOfDay != null && analyserSettings.EndOfDay != null)
            {
                //Then get all the periods that fall in the permitted time, and order them by start time.
                periods = periods.Where(x => x.End.TimeOfDay > analyserSettings.StartOfDay && x.Start.TimeOfDay < analyserSettings.EndOfDay).OrderBy(x => x.Start).ToList();
                if (periods != null && periods.Any())
                {
                    //If the first period starts before the Start time of analyser then remove the duration out of the permitted time.
                    if (periods.First().Start.TimeOfDay < analyserSettings.StartOfDay)
                    {
                        periods.First().Start = periods.First().Start.Add(analyserSettings.StartOfDay - periods.First().Start.TimeOfDay);
                    }

                    //If the last period ends after the End time of analyser then remove the duration out of the permitted time.
                    if (periods.Last().End.TimeOfDay > analyserSettings.EndOfDay)
                    {
                        periods.Last().End = periods.Last().End.Subtract(periods.Last().End.TimeOfDay - analyserSettings.EndOfDay);
                    }
                }
            }
            return periods;
        }

        /// <summary>
        /// Gets the undocumented period.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="analyserSettings"></param>
        /// <returns></returns>
        public static List<Period> GetUnDocumentedPeriodWithRating(List<Period> periods, AnalyserSettings analyserSettings)
        {
            List<Period> undocumentedPeriods = null;
            if (periods != null && periods.Any())
            {
                undocumentedPeriods = new List<Period>();
                //Get all the undocumented period from the start of the day 00:00:00 to end of the day 23:59:59
                for (int i = 0; i < periods.Count; i++)
                {
                    //First undocumented period
                    if (i == 0)
                    {
                        undocumentedPeriods.Add(new Period { End = periods[0].Start, Start = periods[0].Start.Date + new TimeSpan(0, 0, 0) });
                    }

                    //Last undocumented period
                    else if (i == periods.Count - 1)
                    {
                        undocumentedPeriods.Add(new Period { End = periods[i].Start, Start = periods[i - 1].End });
                        undocumentedPeriods.Add(new Period { End = periods[i].Start.Date + new TimeSpan(23, 59, 59), Start = periods[i].End });
                    }

                    else
                    {
                        undocumentedPeriods.Add(new Period { End = periods[i].Start, Start = periods[i - 1].End });
                    }
                }
                if (undocumentedPeriods.Any() && analyserSettings.StartOfDay != null && analyserSettings.EndOfDay != null)
                {
                    //Then get all the periods that fall in the permitted time, and order them by start time.
                    undocumentedPeriods = undocumentedPeriods.Where(x => x.End.TimeOfDay > analyserSettings.StartOfDay && x.Start.TimeOfDay < analyserSettings.EndOfDay).OrderBy(x => x.Start).ToList();
                    if (undocumentedPeriods != null && undocumentedPeriods.Any())
                    {
                        //If the first period starts before the Start time of analyser then remove the duration out of the permitted time.
                        if (undocumentedPeriods.First().Start.TimeOfDay < analyserSettings.StartOfDay)
                        {
                            undocumentedPeriods.First().Start = undocumentedPeriods.First().Start.Add(analyserSettings.StartOfDay - undocumentedPeriods.First().Start.TimeOfDay);
                        }

                        //If the last period ends after the End time of analyser then remove the duration out of the permitted time.
                        if (undocumentedPeriods.Last().End.TimeOfDay > analyserSettings.EndOfDay)
                        {
                            undocumentedPeriods.Last().End = undocumentedPeriods.Last().End.Subtract(undocumentedPeriods.Last().End.TimeOfDay - analyserSettings.EndOfDay);
                        }
                    }
                }
            }
            return undocumentedPeriods;
        }

        public static decimal CalculateOverallWeightedRating(IReadOnlyCollection<PeriodRating> periodRatings)
        {
            decimal rating = 0.0m;
            if (periodRatings != null && periodRatings.Any())
            {

                var weightedSum = periodRatings.Select(x => x.Duration * x.Rating).Sum();
                return weightedSum / periodRatings.Sum(x => x.Duration);
            }
            return rating;
        }
    }
}
