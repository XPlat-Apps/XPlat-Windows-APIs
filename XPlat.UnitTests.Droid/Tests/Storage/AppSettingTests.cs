using System;
using NUnit.Framework;


namespace XPlat.UnitTests.Droid
{
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

            var storedValue = values[settingKey];
        }
    }
}