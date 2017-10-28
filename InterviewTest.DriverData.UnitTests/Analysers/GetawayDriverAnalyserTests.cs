using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class GetawayDriverAnalyserTests
	{
        private AnalyserSettings analyserSettings { get; set; }
        public GetawayDriverAnalyserTests()
        {
            analyserSettings = new AnalyserSettings { StartOfDay = new TimeSpan(13, 0, 0), EndOfDay = new TimeSpan(14, 0, 0), SpeedLimit = 80, ExceedSpeedLimitRating = 1 };
        }
        [Test]
		public void ShouldYieldCorrectValues()
		{
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromHours(1),
				DriverRating = 0.1813m
			};

			var actualResult = new GetawayDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.History);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}
        [Test]
        public void ShouldYieldCorrectValuesCannedDataFromFile()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = TimeSpan.FromHours(1),
                DriverRating = 0.1813m
            };

            //Act
            var actualResult = new GetawayDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.GetHistoryData());

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
            var actualResult = new GetawayDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.EmptyHistory);

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
            var actualResult = new GetawayDriverAnalyser(analyserSettings).Analyse(null);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldReturnZeroRatingForOutOfTimePeriods()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 0, 0),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new GetawayDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.GetawayDriverOutOfPermittedTime);

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
                AnalysedDuration = new TimeSpan(0, 59, 59),
                DriverRating = 1.0m
            };

            //Act
            var actualResult = new GetawayDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.GetAwayDriverExceedSpeedLimit);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
