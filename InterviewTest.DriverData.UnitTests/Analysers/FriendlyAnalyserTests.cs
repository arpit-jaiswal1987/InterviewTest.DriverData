using System;
using InterviewTest.DriverData.Analysers;
using NUnit.Framework;

namespace InterviewTest.DriverData.UnitTests.Analysers
{
	[TestFixture]
	public class FriendlyAnalyserTests
	{
		[Test]
		public void ShouldAnalyseWholePeriodAndReturn1ForDriverRating()
		{
            // BONUS: What is AAA?
            //AAA- Arrange, Act, Assert is a way of formatting and arranging code in C#, so that it is more readable and meaningful.
            //Arrange: Arrange all necessary preconditions and inputs. 
            //Act: Act on the object or method under test.
            //Assert: Assert that the expected results have occurred.

            var data = new[]
			{
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					AverageSpeed = 20m
				},
				new Period
				{
					Start = new DateTimeOffset(2001, 1, 1, 12, 0, 0, TimeSpan.Zero),
					End = new DateTimeOffset(2001, 1, 2, 0, 0, 0, TimeSpan.Zero),
					AverageSpeed = 15m
				}
			};

			var expectedResult = new HistoryAnalysis
			{
				AnalysedDuration = TimeSpan.FromDays(1),
				DriverRating = 1m
			};

			var actualResult = new FriendlyAnalyser().Analyse(data);

			Assert.That(actualResult.AnalysedDuration, Is.EqualTo(expectedResult.AnalysedDuration));
			Assert.That(actualResult.DriverRating, Is.EqualTo(expectedResult.DriverRating));
		}
	}
}
