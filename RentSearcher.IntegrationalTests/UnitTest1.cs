using Microsoft.Extensions.Logging.Abstractions;
using RentSearcher.Crawler.Connectors;

namespace RentSearcher.IntegrationalTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var sRealityGate = new Sreality(NullLogger.Instance);
            var response = sRealityGate.FetchPageUrls().Result;
            Assert.Pass();
        }
    }
}