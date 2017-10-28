using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests
{
    [TestFixture]
    public class FileInputRetrieverTests
    {
        private static readonly string HistoryDataFilesBasePath = ConfigurationSettings.AppSettings.Get("HistoryDataFilesBasePath");

        [Test]
        public void ShouldReturnTextForValidPath()
        {
            //Arrange
            var path = Path.Combine(HistoryDataFilesBasePath, "History.json");
            //Act
            var text = InputRetrieverLookup.GetInputRetriever().RetrieveText(path);
            //Assert
            Assert.IsNotEmpty(text);
        }

        [Test]
        public void ShouldThrowErrorForInvalidPath()
        {
            //Arrange
            var path = Path.Combine(HistoryDataFilesBasePath, "NotFound.json");
            //Act
            //Assert
            Assert.Throws(typeof(FileNotFoundException), delegate { InputRetrieverLookup.GetInputRetriever().RetrieveText(path); });
        }
    }
}
