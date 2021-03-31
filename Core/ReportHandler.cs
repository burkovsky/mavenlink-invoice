using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using Abstractions;

using CsvHelper;

using Microsoft.AspNetCore.Http;

namespace Core
{
    public class ReportHandler : IReportHandler
    {
        public IReadOnlyList<ITimeEntry> ProcessReport(IFormFile file, out string message)
        {
            message = string.Empty;

            if (file == null || file.Length == 0)
            {
                return Array.Empty<ITimeEntry>();
            }

            var entries = new List<TimeEntry>();

            using (Stream stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    entries = csv.GetRecords<TimeEntry>().ToList();
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }

            return entries;
        }
    }
}
