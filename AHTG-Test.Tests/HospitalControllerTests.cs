using AHTG_Test.Controllers;
using AHTG_Test.Data;
using AHTG_Test.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AHTG_Test.Tests
{
    [TestClass]
    public class HospitalControllerTests
    {
        IHospitalRepository _mockHospitalRepository;
        IWebHostEnvironment _mockHostingEnv;
        ICache _mockCache;
        ILogger<HospitalsController> _mockLogger;

        [TestInitialize]
        public void TestInit()
        {
            _mockHospitalRepository = Mock.Of<IHospitalRepository>();
            _mockHostingEnv = Mock.Of<IWebHostEnvironment>();
            _mockCache = Mock.Of<ICache>();
            _mockLogger = Mock.Of<ILogger<HospitalsController>>();

            Mock.Get(_mockHostingEnv)
                .Setup(mock => mock.EnvironmentName)
                .Returns("Development");
        }

        private HospitalsController CreateInstance() 
        {
            return new HospitalsController(_mockHospitalRepository, _mockHostingEnv, _mockCache, _mockLogger);
        }

        [TestMethod]
        public void CanCreate()
        {
            var controller = CreateInstance();
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public async Task GetHospital_DoesNotCallRepositoryIfCacheIsPopualted()
        {
            var controller = CreateInstance();

            var hospitals = new List<Hospital>();
            hospitals.Add(new Hospital());

            Mock.Get(_mockCache)
                .Setup(mock => mock.GetHospitalsAsync())
                .Returns(Task.FromResult<List<Hospital>?>(hospitals));

            await controller.GetHospital();

            Mock.Get(_mockCache).Verify(mock => mock.GetHospitalsAsync(), Times.Once);
            Mock.Get(_mockHospitalRepository).Verify(mock => mock.GetHospitalsAsync(), Times.Never);
        }

        [TestMethod]
        public async Task GetHospital_CallsRepositoryIfCacheIsNotPopulatedAndPopulatesCache()
        {
            var controller = CreateInstance();

            Mock.Get(_mockCache)
                .Setup(mock => mock.GetHospitalsAsync())
                .Returns(Task.FromResult<List<Hospital>?>(null));

            var hospitals = new List<Hospital>();
            hospitals.Add(new Hospital());

            Mock.Get(_mockHospitalRepository)
                .Setup(mock => mock.GetHospitalsAsync())
                .Returns(Task.FromResult<List<Hospital>?>(hospitals));

            await controller.GetHospital();

            Mock.Get(_mockCache).Verify(mock => mock.GetHospitalsAsync(), Times.Once);
            Mock.Get(_mockHospitalRepository).Verify(mock => mock.GetHospitalsAsync(), Times.Once);
            Mock.Get(_mockCache).Verify(mock => mock.PopulateHospitalsAsync(hospitals), Times.Once);
        }
    }
}