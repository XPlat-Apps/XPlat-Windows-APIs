namespace XPlat.NuGet.Droid.Models
{
    using System;

    public class Test
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Test NestedTest { get; set; }
    }
}