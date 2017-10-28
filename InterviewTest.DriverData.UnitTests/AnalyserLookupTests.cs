using InterviewTest.DriverData.Analysers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.UnitTests
{
    [TestFixture]
    public class AnalyserLookupTests
    {
        [Test]
        public void ShouldGetDeliveryDriverAnalyser()
        {
            //Assign
            var analyserType = "delivery_driver";
            //Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.AreEqual(typeof(DeliveryDriverAnalyser), analyser.GetType());
        }
        [Test]
        public void ShouldGetFormulaOneDriverAnalyser()
        {
            //Assign
            var analyserType = "formula_one_driver";
            //Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.AreEqual(typeof(FormulaOneAnalyser), analyser.GetType());
        }
        [Test]
        public void ShouldGetGetawayDriverAnalyser()
        {
            //Assign
            var analyserType = "getaway_driver";
            //Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.AreEqual(typeof(GetawayDriverAnalyser), analyser.GetType());
        }

        [Test]
        public void ShouldGetDeliveryDriverWithPenaltyAnalyser()
        {
            //Assign
            var analyserType = "delivery_driver_with_penalty";
            //Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.AreEqual(typeof(DeliveryDriverAnalyser), analyser.GetType());
        }
        [Test]
        public void ShouldGetFormulaOneWithPenaltyDriverAnalyser()
        {
            //Assign
            var analyserType = "formula_one_driver_with_penalty";
            //Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.AreEqual(typeof(FormulaOneAnalyser), analyser.GetType());
        }
        [Test]
        public void ShouldGetGetawayDriverWithPenaltyAnalyser()
        {
            //Assign
            var analyserType = "getaway_driver_with_penalty";
            //Act
            var analyser = AnalyserLookup.GetAnalyser(analyserType);
            //Assert
            Assert.AreEqual(typeof(GetawayDriverAnalyser), analyser.GetType());
        }
        [Test]
        public void ShouldThrowErrorForWrongInput()
        {
            //Assign
            var analyserType = "invalid";
            //Act
            //Assert
            Assert.Throws(typeof(ArgumentOutOfRangeException), delegate { AnalyserLookup.GetAnalyser(analyserType); });
        }
    }
}
