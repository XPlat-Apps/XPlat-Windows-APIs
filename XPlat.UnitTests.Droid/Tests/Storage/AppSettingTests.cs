namespace XPlat.UnitTests.Droid.Tests.Storage
{
    using NUnit.Framework;

    using XPlat.Storage;

    [TestFixture]
    public class AppSettingTests
    {
        [Test]
        public void Settings_ReturnsValues()
        {
            const string SettingKey = "Hello";
            const int SettingValue = 100;

            ApplicationData.Current.LocalSettings.Values.Add(SettingKey, SettingValue);

            var values = ApplicationData.Current.LocalSettings.Values;

            Assert.IsTrue(values.ContainsKey(SettingKey));

            var storedValue = values[SettingKey];

            Assert.AreEqual(SettingValue, storedValue);
        }
    }
}