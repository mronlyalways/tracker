using System;
using TrackerDemo.ViewModel;
using TrackerDemo.Data;
using Moq;
using NUnit.Framework;
using TrackerDemo.Model;
using System.Collections.Generic;

namespace TrackerDemoTest
{
    [TestFixture]
    public class HomeViewModelTest
    {
        private HomeViewModel home;

        [SetUp]
        public void Setup()
        {
            var mockData = new Mock<IDataService>();
            mockData.Setup(x => x.Load()).Returns(new List<Category>());
            var mockViewModel = new Mock<ChromeViewModel>();
            home = new HomeViewModel(mockData.Object, mockViewModel.Object);
        }

        [Test]
        public void TestMethod1()
        {
            Assert.Fail();
        }
    }
}
