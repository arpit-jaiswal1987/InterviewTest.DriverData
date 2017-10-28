using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class DeliveryDriverAnalyserTests
	{
        private AnalyserSettings analyserSettings { get; set; }
        public DeliveryDriverAnalyserTests()
        {
            analyserSettings = new AnalyserSettings { StartOfDay = new TimeSpan(9, 0, 0), EndOfDay = new TimeSpan(17,0,0), SpeedLimit = 30 };
        }
        [Test]
		public void ShouldYieldCorrectValues()
		{
            //Arrange
			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = new TimeSpan(7, 45, 0),
				DriverRating = 0.7638m
			};

            //Act
			var actualResult = new DeliveryDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.History);

            //Assert
			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
		}

        [Test]
        public void ShouldYieldCorrectValuesCannedDataFromFile()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(7, 45, 0),
                DriverRating = 0.7638m
            };

            //Act
            var actualResult = new DeliveryDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.GetHistoryData());

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldReturnZeroRatingForEmptyListOfPeriod()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis {
                AnalysedDuration=new TimeSpan(0,0,0),
                DriverRating=0.0m
            };

            //Act
            var actualResult = new DeliveryDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.EmptyHistory);

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
            var actualResult = new DeliveryDriverAnalyser(analyserSettings).Analyse(null);

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
            var actualResult = new DeliveryDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.DeliveryDriverOutOfPermittedTime);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }

        [Test]
        public void ShouldReturnZeroRatingForExceedingSpeedLimit()
        {
            //Arrange
            var expectedResult = new HistoryAnalysis
            {
                AnalysedDuration = new TimeSpan(0, 59, 59),
                DriverRating = 0.0m
            };

            //Act
            var actualResult = new DeliveryDriverAnalyser(analyserSettings).Analyse(CannedDrivingData.DeliveryDriverExceedSpeedLimit);

            //Assert
            Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
            Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating).Within(0.001m));
        }
    }
}
