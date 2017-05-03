namespace XPlat.UnitTests.iOS.Tests.Storage
{
    using NUnit.Framework;

    using XPlat.Storage;

    [TestFixture]
    public class AppSettingTests
    {
        [Test]
        public void AppSettingsReturnsAllItems()
        {
            string settingKey = "Hello";
            int settingValue = 100;

            ApplicationData.Current.LocalSettings.AddOrUpdate(settingKey, settingValue);

            var values = ApplicationData.Current.LocalSettings.Values;

            Assert.IsTrue(values.ContainsKey(settingKey));
        }
    }
}
