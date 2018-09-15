using Microsoft.VisualStudio.TestTools.UnitTesting;
using XPlat.Helpers;

namespace XPlat.UnitTests.Core
{
    [TestClass]
    public class RequestCodeHelperTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            RequestCodeHelper.Reset();
        }

        [TestMethod]
        public void RequestCodeHelper_GenerateRequestCode_ReturnsNewCode()
        {
            int lastRequestCode = RequestCodeHelper.LastRequestCode;
            int newRequestCode = RequestCodeHelper.GenerateRequestCode();

            Assert.AreEqual(lastRequestCode + 1, newRequestCode);
        }

        [TestMethod]
        public void RequestCodeHelper_GenerateRequestCode_AfterMaxValueShouldReturnOne()
        {
            int lastRequestCode = RequestCodeHelper.LastRequestCode;

            int newRequestCode = 0;

            // Simulates generating a request code enough times to exceed the maximum value by one.
            for (int i = lastRequestCode; i <= ushort.MaxValue; i++)
            {
                newRequestCode = RequestCodeHelper.GenerateRequestCode();
            }

            Assert.AreEqual(1, newRequestCode);
        }
    }
}