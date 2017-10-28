using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FormulaOneAnalyserTests
	{
        private AnalyserSettings analyserSettings { get; set; }
        public FormulaOneAnalyserTests()
        {
            analyserSettings = new AnalyserSettings { StartOfDay = TimeSpan.Zero, EndOfDay = TimeSpan.Zero, SpeedLimit = 200, ExceedSpeedLimitRating = 1 };
        }

        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(10, 3, 0),
				DriverRating = 0.1231m
			};

			var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}
        [Test]
        public void ShouldYieldCorrectValuesCannedDataFromFile()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(10, 3, 0),
                DriverRating = 0.1231m
            };

            //Act
            var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(CannedDrivingData.GetHistoryData());

            //Assert
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
        public void ShouldReturnOneRatingForExceedingSpeedLimit()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(1, 0, 0),
                DriverRating = 1.0m
            };

            //Act
            var actualResult = new FormulaOneAnalyser(analyserSettings).Analyse(CannedDrivingData.FormulaOneDriverExceedSpeedLimit);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
