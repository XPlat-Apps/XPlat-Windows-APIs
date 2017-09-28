namespace XPlat.UnitTests.UWP.Tests.Storage
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using XPlat.Storage;
    using XPlat.UnitTests.UWP.Mocks;

    [TestClass]
    public class AppSettingTests
    {
        [TestMethod]
        public void LocalSettings_Exist()
        {
            Assert.IsNotNull(ApplicationData.Current.LocalSettings);
        }

        [TestMethod]
        public void LocalSettings_Values_Exist()
        {
            Assert.IsNotNull(ApplicationData.Current.LocalSettings.Values);
        }

        [TestMethod]
        public void LocalSettings_Values_CreateContainerNotSupported()
        {
            Assert.ThrowsException<NotSupportedException>(
                () =>
                    {
                        ApplicationData.Current.LocalSettings.CreateContainer(
                            "Test",
                            ApplicationDataCreateDisposition.Always);
                    });
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_StringSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_StringSupported";
            string expectedValue = "Hello, World!";

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_StringSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_StringSupported";
            string expectedValue = "Hello, World!";

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_StringSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_StringSupported";
            string expectedValue = "Hello, World!";


            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_BooleanSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_BooleanSupported";
            bool expectedValue = true;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_BooleanSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_BooleanSupported";
            bool expectedValue = true;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_BooleanSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_BooleanSupported";
            bool expectedValue = true;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_Int32Supported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_Int32Supported";
            int expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_Int32Supported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_Int32Supported";
            int expectedValue = 100;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_Int32Supported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_Int32Supported";
            int expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_Int64Supported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_Int64Supported";
            long expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_Int64Supported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_Int64Supported";
            long expectedValue = 100;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_Int64Supported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_Int64Supported";
            long expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_FloatSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_FloatSupported";
            float expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_FloatSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_FloatSupported";
            float expectedValue = 100;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_FloatSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_FloatSupported";
            float expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_DoubleSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_DoubleSupported";
            double expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_DoubleSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_DoubleSupported";
            double expectedValue = 100;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_DoubleSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_DoubleSupported";
            double expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_DecimalSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_DecimalSupported";
            decimal expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_DecimalSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_DecimalSupported";
            decimal expectedValue = 100;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_DecimalSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_DecimalSupported";
            decimal expectedValue = 100;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_DateTimeSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_DateTimeSupported";
            DateTime expectedValue = DateTime.Now;

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_DateTimeSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_DateTimeSupported";
            DateTime expectedValue = DateTime.Now;

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_DateTimeSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_DateTimeSupported";
            DateTime expectedValue = DateTime.Now;

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_GuidSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_GuidSupported";
            Guid expectedValue = Guid.NewGuid();

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue.ToString(), actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_GuidSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_GuidSupported";
            Guid expectedValue = Guid.NewGuid();

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue.ToString(), actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_GuidSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_GuidSupported";
            Guid expectedValue = Guid.NewGuid();

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue.ToString(), actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyAndValue_ObjectSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyAndValue_ObjectSupported";
            AppSetting expectedValue = new AppSetting
                                           {
                                               Name = "Hello, World!",
                                               Date = DateTime.Now,
                                               NestedSetting = new AppSetting { Name = "Nested" }
                                           };

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, expectedValue);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_KeyValuePair_ObjectSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_KeyValuePair_ObjectSupported";
            AppSetting expectedValue = new AppSetting
                                           {
                                               Name = "Hello, World!",
                                               Date = DateTime.Now,
                                               NestedSetting = new AppSetting { Name = "Nested" }
                                           };

            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            ApplicationData.Current.LocalSettings.Values.Add(keyValuePair);

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Add_ViaKeyIndex_ObjectSupported()
        {
            string expectedKey = "LocalSettings_Values_Add_ViaKeyIndex_ObjectSupported";
            AppSetting expectedValue = new AppSetting
                                           {
                                               Name = "Hello, World!",
                                               Date = DateTime.Now,
                                               NestedSetting = new AppSetting { Name = "Nested" }
                                           };

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_GetValue_ViaGetMethod()
        {
            string expectedKey = "LocalSettings_Values_GetValue_ViaGetMethod";
            AppSetting expectedValue = new AppSetting
                                           {
                                               Name = "Hello, World!",
                                               Date = DateTime.Now,
                                               NestedSetting = new AppSetting { Name = "Nested" }
                                           };

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            AppSetting actualValue = ApplicationData.Current.LocalSettings.Values.Get<AppSetting>(expectedKey);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void LocalSettings_Values_Remove_RemovedValueDoesNotExist()
        {
            string expectedKey = "LocalSettings_Values_Remove_RemovedValueDoesNotExist";
            AppSetting expectedValue = new AppSetting
                                           {
                                               Name = "Hello, World!",
                                               Date = DateTime.Now,
                                               NestedSetting = new AppSetting { Name = "Nested" }
                                           };

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            AppSetting actualValue = ApplicationData.Current.LocalSettings.Values.Get<AppSetting>(expectedKey);
            Assert.AreEqual(expectedValue, actualValue);

            ApplicationData.Current.LocalSettings.Values.Remove(expectedKey);
            Assert.IsFalse(ApplicationData.Current.LocalSettings.Values.ContainsKey(expectedKey));
        }

        [TestMethod]
        public void LocalSettings_Values_Update_ChangedValueUpdatesSameKey()
        {
            string expectedKey = "LocalSettings_Values_Update_ChangedValueUpdatesSameKey";
            string originalValue = "Hello, World!";
            string expectedValue = "What's up doc?";

            ApplicationData.Current.LocalSettings.Values.Add(expectedKey, originalValue);

            object originalActualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];

            Assert.AreEqual(originalValue, originalActualValue);

            ApplicationData.Current.LocalSettings.Values[expectedKey] = expectedValue;

            object actualValue = ApplicationData.Current.LocalSettings.Values[expectedKey];

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}