using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests.Helpers
{
    [TestFixture]
    public class JsonSerializerTests
    {
        [Test]
        public void ShouldReturnValidObject()
        {
            //Arrange
            var jsonString = "[{\"Start\":\"10/13/2016 9:28:00 AM +00:00\",\"End\":\"10/13/2016 9:35:00 AM +00:00\",\"Average\":\"0\"  }]";
            //Act
            var result = (List<Period>)SerializerLookup.GetSerializer().Deserialize(jsonString, typeof(List<Period>));
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(List<Period>), result.GetType());
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void ShouldThrowExceptionForInvalidJSON()
        {
            //Arrange
            var jsonString = "[{";
            //Act
            //Assert
            Assert.Throws(typeof(FormatException), delegate { var text = (List<Period>)SerializerLookup.GetSerializer().Deserialize(jsonString, typeof(List<Period>)); });

        }
    }
}
