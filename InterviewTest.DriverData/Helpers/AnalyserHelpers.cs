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
        internal static List<Period> GetValidPeriods(List<Period> periods, AnalyserSettings analyserSettings)
        {
            //Clone periods(HistoryData) so that the data inside the object does not change.
            var validPeriods = CloneObject(periods);
            //If the Start and End time for analyser is set.
            if (validPeriods != null && validPeriods.Any() && analyserSettings != null && analyserSettings.StartOfDay != null && analyserSettings.StartOfDay != TimeSpan.Zero && analyserSettings.EndOfDay != null && analyserSettings.EndOfDay != TimeSpan.Zero)
            {
                //Then get all the periods that fall in the permitted time, and order them by start time.
                validPeriods = validPeriods.Where(x => x.End.TimeOfDay > analyserSettings.StartOfDay && x.Start.TimeOfDay < analyserSettings.EndOfDay).OrderBy(x => x.Start).ToList();
                if (validPeriods != null && validPeriods.Any())
                {
                    //If the first period starts before the Start time of analyser then remove the duration out of the permitted time.
                    if (validPeriods.First().Start.TimeOfDay < analyserSettings.StartOfDay)
                    {
                        validPeriods.First().Start = validPeriods.First().Start.Add(analyserSettings.StartOfDay - validPeriods.First().Start.TimeOfDay);
                    }

                    //If the last period ends after the End time of analyser then remove the duration out of the permitted time.
                    if (validPeriods.Last().End.TimeOfDay > analyserSettings.EndOfDay)
                    {
                        validPeriods.Last().End = validPeriods.Last().End.Subtract(validPeriods.Last().End.TimeOfDay - analyserSettings.EndOfDay);
                    }
                }
            }
            else if (validPeriods != null && validPeriods.Any())
            {
                validPeriods = validPeriods.OrderBy(x => x.Start).ToList();
            }
            return validPeriods;
        }

        internal static IEnumerable<PeriodRating> CalculateRatingForValidPeriods(List<Period> validPeriods, AnalyserSettings analyserSettings, out TimeSpan analysedDuration)
        {
            var periodRatings = new List<PeriodRating>();
            analysedDuration = new TimeSpan(0, 0, 0);
            foreach (var validPeriod in validPeriods)
            {
                if (validPeriod.AverageSpeed > analyserSettings.SpeedLimit)
                {
                    periodRatings.Add(new DriverData.PeriodRating { Rating = analyserSettings.ExceedSpeedLimitRating, Duration = (decimal)((validPeriod.End.TimeOfDay - validPeriod.Start.TimeOfDay).TotalSeconds) });
                }
                else
                {
                    periodRatings.Add(new DriverData.PeriodRating { Rating = validPeriod.AverageSpeed / analyserSettings.SpeedLimit, Duration = (decimal)((validPeriod.End - validPeriod.Start).TotalSeconds) });
                }
                analysedDuration += (validPeriod.End - validPeriod.Start);
            }
            return periodRatings;
        }

        /// <summary>
        /// Gets the undocumented period.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="analyserSettings"></param>
        /// <returns></returns>
        internal static List<Period> GetUnDocumentedPeriodWithRating(List<Period> periods, AnalyserSettings analyserSettings)
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
                        undocumentedPeriods.Add(new Period { End = periods[0].Start, Start = periods[0].Start - periods[0].Start.TimeOfDay });
                    }

                    //Last undocumented period
                    else if (i == periods.Count - 1)
                    {
                        undocumentedPeriods.Add(new Period { End = periods[i].Start, Start = periods[i - 1].End });
                        undocumentedPeriods.Add(new Period { End = periods[i].Start - periods[i].Start.TimeOfDay + new TimeSpan(23, 59, 59), Start = periods[i].End });
                    }

                    else
                    {
                        undocumentedPeriods.Add(new Period { End = periods[i].Start, Start = periods[i - 1].End });
                    }
                }
                //Remove the undocumented period where start and end are same.
                if (undocumentedPeriods.Any())
                {
                    undocumentedPeriods.RemoveAll(x => x.Start == x.End);
                }

                if (undocumentedPeriods.Any() && analyserSettings.StartOfDay != null && analyserSettings.StartOfDay != TimeSpan.Zero && analyserSettings.EndOfDay != null && analyserSettings.EndOfDay != TimeSpan.Zero)
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

        internal static IEnumerable<PeriodRating> CalculateRatingForUndocumentedPeriods(List<Period> undocumentPeriods)
        {
            var periodRatings = new List<PeriodRating>();
            foreach (var undocumentedPeriod in undocumentPeriods)
            {
                periodRatings.Add(new DriverData.PeriodRating { Rating = 0.0m, Duration = (decimal)((undocumentedPeriod.End - undocumentedPeriod.Start).TotalSeconds) });
            }
            return periodRatings;
        }

        internal static decimal CalculateOverallWeightedRating(IReadOnlyCollection<PeriodRating> periodRatings, AnalyserSettings analyserSettings,bool isUndocumentedPeriodAvailable)
        {
            decimal rating = 0.0m;
            if (periodRatings != null && periodRatings.Any())
            {

                var weightedSum = periodRatings.Select(x => x.Duration * x.Rating).Sum();
                rating = weightedSum / periodRatings.Sum(x => x.Duration);
                if (isUndocumentedPeriodAvailable && analyserSettings.ApplyUnDocumentedPenaltyFlag)
                {
                    rating = rating * analyserSettings.UndocumentedPenalty;
                }
            }
            return rating;
        }

        private static T CloneObject<T>(T obj)
        {
            T cloneObj = default(T);
            if (obj != null)
            {
                var serializer = SerializerLookup.GetSerializer();
                var serializedString = serializer.Serialize(obj);
                cloneObj = (T)serializer.Deserialize(serializedString, typeof(T));
            }
            return cloneObj;
        }
    }
}
