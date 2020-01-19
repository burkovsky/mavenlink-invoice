using System;

using Abstractions;

using CsvHelper.Configuration.Attributes;

namespace Core
{
    public class TimeEntry : ITimeEntry
    {
        [Index(0)]
        public DateTime Date { get; set; }

        [Index(1)]
        public string Person { get; set; }

        [Index(2)]
        public string Project { get; set; }

        [Index(3)]
        public string Task { get; set; }

        [Index(4)]
        public string Notes { get; set; }

        [Index(5)]
        public string Location { get; set; }

        [Index(6)]
        public string Status { get; set; }

        [Index(7)]
        public float Hours { get; set; }

        [Index(9)]
        public int Minutes { get; set; }
    }
}
