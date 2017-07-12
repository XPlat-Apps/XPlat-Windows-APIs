namespace XPlat.NuGet.UWP.Models
{
    using System;

    public class Test
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Test NestedTest { get; set; }
    }
}