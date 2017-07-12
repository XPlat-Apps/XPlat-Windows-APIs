XPlat Windows APIs
===========

XPlat Windows APIs are designed to make it easier for UWP developers to build for iOS and Android taking advantage of common APIs found in the Windows APIs without a great deal of change to their existing Windows app code. 

The design for these APIs are based on the interfaces within the Windows SDK allowing an ease to porting a current Universal Windows Platform application to Xamarin iOS and Android.

For example, if your application takes advantage of the Windows.Storage (e.g. Windows.Storage.ApplicationData.Current.LocalFolder), we provide a XPlat.Storage API for those (e.g. XPlat.Storage.ApplicationData.Current.LocalFolder) which you can use in your Xamarin iOS and Android applications. 

## Project build status

| Branch | Status |
| ------ | ------ |
| develop | [![Status](https://jamesmcroft.visualstudio.com/_apis/public/build/definitions/4cfe114a-c08f-45a4-91ee-3260703e08dd/14/badge)](https://github.com/jamesmcroft/XPlat-Windows-APIs/tree/develop) |
| master | [![Status](https://jamesmcroft.visualstudio.com/_apis/public/build/definitions/4cfe114a-c08f-45a4-91ee-3260703e08dd/14/badge)](https://github.com/jamesmcroft/XPlat-Windows-APIs/tree/master) | 

## Install via NuGet
Looking to get these components in your apps now? Well, you can get your hands on the components via NuGet.

| Package | Description | Version |
| ------ | ------ | ------ |
| XPlat.Core | This library contains a collection of core/common APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Core.svg)](https://www.nuget.org/packages/XPlat.Core/) |
| XPlat.UI.Core | This library contains a collection of Windows.UI.Core APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.UI.Core.svg)](https://www.nuget.org/packages/XPlat.UI.Core/) |
| XPlat.Storage | This library contains a collection of Windows.Storage APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Storage.svg)](https://www.nuget.org/packages/XPlat.Storage/) |
| XPlat.Media | This library contains a collection of Windows.Media APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Media.svg)](https://www.nuget.org/packages/XPlat.Media/) |
| XPlat.Device.Display | This library contains a collection of Windows.System.Display APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Device.Display.svg)](https://www.nuget.org/packages/XPlat.Device.Display/) |
| XPlat.Device.Geolocation | This library contains a collection of Windows.Device.Geolocation APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Device.Geolocation.svg)](https://www.nuget.org/packages/XPlat.Device.Geolocation/) |
| XPlat.Device.Power | This library contains a collection of Windows.System.Power APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Device.Power.svg)](https://www.nuget.org/packages/XPlat.Device.Power/) |
| XPlat.Device.Launcher | This library contains a collection of Windows.System.Launcher APIs. | [![NuGet](https://img.shields.io/nuget/v/XPlat.Device.Launcher.svg)](https://www.nuget.org/packages/XPlat.Device.Launcher/) |

## Contributing
Do you want to contribute? Check out the [contribution guidelines](CONTRIBUTING.md) for more info.

## License
XPlat Windows APIs are made available under the terms and conditions of the [MIT license](LICENSE). 
