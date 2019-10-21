<img src="Assets/ProjectBanner.png" alt="XPlat Windows APIs" />

# XPlat Windows APIs

XPlat Windows APIs are designed to make it easier for developers adjusted to developing with the Windows UWP APIs to take those skills cross-platform with their  applications.

As each application platform offers their own unique platform-specific APIs, XPlat attempts to bring all of those APIs under one umbrella using a Windows UWP like API, allowing a developer to learn a single API which works with any application built with UWP or Xamarin for Android and iOS. 

For a UWP developer, this is appealing as it allows you to easily migrate your existing UWP code to Xamarin shared code with minimal changes through the use of XPlat Windows APIs which mimic the Windows UWP alternatives.

## Package status

| Source | Build | Release | Current | Preview |
| ------ | ------ | ------ | ------ | ------ |
| NuGet | [![Build status](https://dev.azure.com/jamesmcroft/XPlat%20Windows%20APIs/_apis/build/status/XPlat.CI)](https://dev.azure.com/jamesmcroft/XPlat%20Windows%20APIs/_build/latest?definitionId=24) | [![Release status](https://vsrm.dev.azure.com/jamesmcroft/_apis/public/Release/badge/ec86cd27-ab77-46ad-8b77-66259dd5a477/1/4)](https://dev.azure.com/jamesmcroft/XPlat%20Windows%20APIs/_build/latest?definitionId=24) | [![Nuget](https://img.shields.io/nuget/v/XPlat.Core.svg)](https://www.nuget.org/packages/XPlat.Core/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Core.svg)](https://www.nuget.org/packages/XPlat.Core/) |

## Installation
XPlat Windows APIs are available via NuGet. Each available package is detailed below. 

We have purposefully split the XPlat Windows APIs to allow you to pick and choose the specific components that your app needs to prevent bloat!

| Package | Current | Preview |
| ------ | ------ | ------ |
| XPlat.ApplicationModel | [![Nuget](https://img.shields.io/nuget/v/XPlat.ApplicationModel.svg)](https://www.nuget.org/packages/XPlat.ApplicationModel/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.ApplicationModel.svg)](https://www.nuget.org/packages/XPlat.ApplicationModel/) |
| XPlat.ApplicationModel.DataTransfer | [![Nuget](https://img.shields.io/nuget/v/XPlat.ApplicationModel.DataTransfer.svg)](https://www.nuget.org/packages/XPlat.ApplicationModel.DataTransfer/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.ApplicationModel.DataTransfer.svg)](https://www.nuget.org/packages/XPlat.ApplicationModel.DataTransfer/) |
| XPlat.Core | [![Nuget](https://img.shields.io/nuget/v/XPlat.Core.svg)](https://www.nuget.org/packages/XPlat.Core/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Core.svg)](https://www.nuget.org/packages/XPlat.Core/) |
| XPlat.Device.Display | [![Nuget](https://img.shields.io/nuget/v/XPlat.Device.Display.svg)](https://www.nuget.org/packages/XPlat.Device.Display/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Device.Display.svg)](https://www.nuget.org/packages/XPlat.Device.Display/) |
| XPlat.Device.Geolocation | [![Nuget](https://img.shields.io/nuget/v/XPlat.Device.Geolocation.svg)](https://www.nuget.org/packages/XPlat.Device.Geolocation/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Device.Geolocation.svg)](https://www.nuget.org/packages/XPlat.Device.Geolocation/) |
| XPlat.Device.Launcher | [![Nuget](https://img.shields.io/nuget/v/XPlat.Device.Launcher.svg)](https://www.nuget.org/packages/XPlat.Device.Launcher/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Device.Launcher.svg)](https://www.nuget.org/packages/XPlat.Device.Launcher/) |
| XPlat.Device.Power | [![Nuget](https://img.shields.io/nuget/v/XPlat.Device.Power.svg)](https://www.nuget.org/packages/XPlat.Device.Power/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Device.Power.svg)](https://www.nuget.org/packages/XPlat.Device.Power/) |
| XPlat.Device.Profile | [![Nuget](https://img.shields.io/nuget/v/XPlat.Device.Profile.svg)](https://www.nuget.org/packages/XPlat.Device.Profile/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Device.Profile.svg)](https://www.nuget.org/packages/XPlat.Device.Profile/) |
| XPlat.Foundation | [![Nuget](https://img.shields.io/nuget/v/XPlat.Foundation.svg)](https://www.nuget.org/packages/XPlat.Foundation/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Foundation.svg)](https://www.nuget.org/packages/XPlat.Foundation/) |
| XPlat.Media.Capture | [![Nuget](https://img.shields.io/nuget/v/XPlat.Media.Capture.svg)](https://www.nuget.org/packages/XPlat.Media.Capture/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Media.Capture.svg)](https://www.nuget.org/packages/XPlat.Media.Capture/) |
| XPlat.Services.Maps | [![Nuget](https://img.shields.io/nuget/v/XPlat.Services.Maps.svg)](https://www.nuget.org/packages/XPlat.Services.Maps/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Services.Maps.svg)](https://www.nuget.org/packages/XPlat.Services.Maps/) |
| XPlat.Storage | [![Nuget](https://img.shields.io/nuget/v/XPlat.Storage.svg)](https://www.nuget.org/packages/XPlat.Storage/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Storage.svg)](https://www.nuget.org/packages/XPlat.Storage/) |
| XPlat.Storage.Pickers | [![Nuget](https://img.shields.io/nuget/v/XPlat.Storage.Pickers.svg)](https://www.nuget.org/packages/XPlat.Storage.Pickers/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.Storage.Pickers.svg)](https://www.nuget.org/packages/XPlat.Storage.Pickers/) |
| XPlat.UI | [![Nuget](https://img.shields.io/nuget/v/XPlat.UI.svg)](https://www.nuget.org/packages/XPlat.UI/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.UI.svg)](https://www.nuget.org/packages/XPlat.UI/) |
| XPlat.UI.Controls | [![Nuget](https://img.shields.io/nuget/v/XPlat.UI.Controls.svg)](https://www.nuget.org/packages/XPlat.UI.Controls/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.UI.Controls.svg)](https://www.nuget.org/packages/XPlat.UI.Controls/) |
| XPlat.UI.Core | [![Nuget](https://img.shields.io/nuget/v/XPlat.UI.Core.svg)](https://www.nuget.org/packages/XPlat.UI.Core/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.UI.Core.svg)](https://www.nuget.org/packages/XPlat.UI.Core/) |
| XPlat.UI.Popups | [![Nuget](https://img.shields.io/nuget/v/XPlat.UI.Popups.svg)](https://www.nuget.org/packages/XPlat.UI.Popups/) | [![Nuget](https://img.shields.io/nuget/vpre/XPlat.UI.Popups.svg)](https://www.nuget.org/packages/XPlat.UI.Popups/) |

Take a look at our '[Getting started with XPlat Windows APIs guide](https://xplat.gitbook.io/docs/)' for help getting up and running!

## Made with XPlat

Got a great project you've built with XPlat? We'd love for you to share your awesome creations with the community!

[*Add your projects to the Made with XPlat collection!*](YOUR-PROJECTS.md)

## Documentation

If you want to deep dive into the APIs with details on how to use the features, you can browse the [GitBooks documentation for XPlat Windows APIs](https://xplat.gitbook.io/docs/)!

## Supported platforms

XPlat Windows APIs is currently being developed for the following platforms:

- Windows UWP
- iOS
- Android

## Contributing 

Looking to help build our XPlat Windows APIs? Take a look through our [contribution guidelines](CONTRIBUTING.md).

We actively encourage you to jump in and help with any issues!

## Building XPlat Windows APIs

XPlat Windows APIs have been built using .NET Standard, taking advantage of the new SDK-style projects and multi-targeting enabled with the help of [MSBuild.Sdk.Extras](https://github.com/onovotny/MSBuildSdkExtras).

You can build the solution using Visual Studio with the following workloads installed:
- .NET desktop development
- Universal Windows Platform development
- Mobile Development with .NET
- .NET Core cross-platform development

## License

XPlat Windows APIs are made available under the terms and conditions of the [MIT license](LICENSE).
