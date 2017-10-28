using System;
using InterviewTest.DriverData.Analysers;
using System.Collections.Generic;
using System.Linq;

namespace InterviewTest.DriverData
{
	public static class AnalyserLookup
	{
        private static Dictionary<Func<string, bool>, IAnalyser> Analysers = new Dictionary<Func<string, bool>, IAnalyser>();
        static AnalyserLookup()
        {
            ConfigureAnalysers();
        }

        /// <summary>
        /// Configure all the analysers that are supported
        /// </summary>
        private static void ConfigureAnalysers()
        {
            AnalyserLookup.RegisterAnalysers(x => x.Equals("friendly"), new FriendlyAnalyser());
            AnalyserLookup.RegisterAnalysers(x => x.Equals("delivery_driver"), new DeliveryDriverAnalyser(new AnalyserSettings { StartOfDay = new TimeSpan(9, 0, 0), EndOfDay = new TimeSpan(17, 0, 0), SpeedLimit = 30, ExceedSpeedLimitRating = 0 }));
            AnalyserLookup.RegisterAnalysers(x => x.Equals("delivery_driver_with_penalty"), new DeliveryDriverAnalyser(new AnalyserSettings { StartOfDay = new TimeSpan(9, 0, 0), EndOfDay = new TimeSpan(17, 0, 0), SpeedLimit = 30, ExceedSpeedLimitRating = 0, ApplyUnDocumentedPenaltyFlag = true, UndocumentedPenalty = 0.5m }));
            AnalyserLookup.RegisterAnalysers(x => x.Equals("formula_one_driver"), new FormulaOneAnalyser(new AnalyserSettings { StartOfDay = TimeSpan.Zero, EndOfDay = TimeSpan.Zero, SpeedLimit = 200, ExceedSpeedLimitRating = 1 }));
            AnalyserLookup.RegisterAnalysers(x => x.Equals("formula_one_driver_with_penalty"), new FormulaOneAnalyser(new AnalyserSettings { StartOfDay = TimeSpan.Zero, EndOfDay = TimeSpan.Zero, SpeedLimit = 200, ExceedSpeedLimitRating = 1, ApplyUnDocumentedPenaltyFlag = true, UndocumentedPenalty = 0.5m }));
            AnalyserLookup.RegisterAnalysers(x => x.Equals("getaway_driver"), new GetawayDriverAnalyser(new AnalyserSettings { StartOfDay = new TimeSpan(13, 0, 0), EndOfDay = new TimeSpan(14, 0, 0), SpeedLimit = 80, ExceedSpeedLimitRating = 1 }));
            AnalyserLookup.RegisterAnalysers(x => x.Equals("getaway_driver_with_penalty"), new GetawayDriverAnalyser(new AnalyserSettings { StartOfDay = new TimeSpan(13, 0, 0), EndOfDay = new TimeSpan(14, 0, 0), SpeedLimit = 80, ExceedSpeedLimitRating = 1, ApplyUnDocumentedPenaltyFlag = true, UndocumentedPenalty = 0.5m }));
        }

        private static void RegisterAnalysers(Func<string, bool> evaluator, IAnalyser analyser)
        {
            Analysers.Add(evaluator, analyser);
        }

        public static IAnalyser GetAnalyser(string type)
        {
            var analyser = Analysers.FirstOrDefault(x => x.Key(type));
            if (analyser.Key != null)
            {
                return analyser.Value;
            }
            throw new ArgumentOutOfRangeException(nameof(type), type, "Unrecognised analyser type");
        }
    }
}
