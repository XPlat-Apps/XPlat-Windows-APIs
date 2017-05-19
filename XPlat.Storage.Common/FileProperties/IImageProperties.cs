namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    public interface IImageProperties
    {
        string Title { get; }

        int Rating { get; }

        string DateTaken { get; }

        string CameraModel { get; }

        string CameraManufacturer { get; }

        double? Latitude { get; }

        double? Longitude { get; }

        PhotoOrientation Orientation { get; }

        IReadOnlyList<string> PeopleNames { get; }

        int Height { get; }

        IList<string> Keywords { get; }

        int Width { get; }
    }
}