using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserPenaltyTests
	{
        private AnalyserSettings analyserSettings { get; set; }
        public FormulaOneAnalyserPenaltyTests()
        {
            analyserSettings = new AnalyserSettings { StartOfDay = TimeSpan.Zero, EndOfDay = TimeSpan.Zero, SpeedLimit = 200, ApplyUnDocumentedPenaltyFlag = true, UndocumentedPenalty = 0.5m, ExceedSpeedLimitRating = 1 };
        }

        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(10, 3, 0),
				DriverRating = 0.1231m*0.5m
			};

			var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void ShouldReturnZeroRatingForEmptyListOfPeriod()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(CannedDrivingData.EmptyHistory);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldReturnZeroRatingForNullParameter()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(null);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
       
        [Test]
        public void ShouldReturnHalfRatingForExceedingSpeedLimit()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(2, 0, 0),
                DriverRating = 1.0m*0.5m
            };

            //Act
            var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(CannedDrivingData.FormulaOneDriverExceedSpeedLimitWithPenalty);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
