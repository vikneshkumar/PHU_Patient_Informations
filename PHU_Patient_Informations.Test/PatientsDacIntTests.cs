using NUnit.Framework;
using PHU_Patient_Informations.DataLayer;

namespace PHU_Patient_Informations.Test
{
    [TestFixture]
    public class PatientsDacIntTests
    {
        private PatientsDac _patientsDac;
        [SetUp]
        public void Setup()
        {
            _patientsDac = new PatientsDac();
        }

        [TestCase("1", 15)]
        [TestCase("2", 10)]
        [TestCase("3", 20)]
        [TestCase("4", 8)]
        public void TotalBedAvailability_ReturnsExpectedResult(string command, int expectedResult)
        {
            // Arrange & Act
            var result = _patientsDac.GetAvailability(command);

            // Assert
            Assert.AreEqual(expectedResult, result.TotalBed);
        }
    }
}