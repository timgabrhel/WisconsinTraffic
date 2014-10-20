using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WITraffic511.Api.Tests
{
    [TestClass]
    public class TrafficClientTests
    {
        private readonly LoggingService _loggingService = new LoggingService();
        private const string ApiKey = "49a9408c46374e55834a78e74b2869b5";
        private ITrafficClient _client;
        private bool _useMock;

        [TestInitialize]
        public void Setup()
        {
            _useMock = true;

            if (_useMock)
            {
                _client = new MockTrafficClient(_loggingService);
            }
            else
            {
                _client = new TrafficClient(ApiKey);
            }
        }

        [TestMethod]
        public async Task GetMainlineRoutesTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetMainlineRoutesAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            //Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetIncidentsTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetIncidentsAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public async Task GetMainlineLinksTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetMainlineLinksAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetWinterRoadConditionsTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetWinterRoadConditionsAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetCamerasTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetCamerasAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetRoadwaysTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetRoadwaysAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetRoadworkTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetRoadworkAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
        }

        [TestMethod]
        public async Task GetMessageSignsTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetMessageSignsAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count > 0);
        }
        
        [TestMethod]
        public async Task GetAlertsTest()
        {
            _client = new TrafficClient(ApiKey);
            var result = await _client.GetAlertsAsync();

            Assert.IsTrue(result.HttpResponseMessage.IsSuccessStatusCode);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.Count > 0);
        }
    }
}
