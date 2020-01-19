using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Abstractions;

namespace Core
{
    public static class TimeEntryExtensions
    {
        public static IReadOnlyDictionary<string, IReadOnlyDictionary<int, IReadOnlyDictionary<string, IReadOnlyDictionary<string, float>>>> ToInvoices(
            this IReadOnlyList<ITimeEntry> entries)
        {
            var dateTimeFormatInfo = new DateTimeFormatInfo();
            var personsDictionary = new SortedDictionary<string, IReadOnlyDictionary<int, IReadOnlyDictionary<string, IReadOnlyDictionary<string, float>>>>();

            if (entries == null)
            {
                return personsDictionary;
            }

            IEnumerable<IGrouping<string, ITimeEntry>> groupsByPerson = entries.GroupBy(e => e.Person);
            foreach (IGrouping<string, ITimeEntry> groupByPerson in groupsByPerson)
            {
                string person = groupByPerson.Key;
                var yearsDictionary = new SortedDictionary<int, IReadOnlyDictionary<string, IReadOnlyDictionary<string, float>>>();
                personsDictionary.Add(person, yearsDictionary);

                IEnumerable<IGrouping<int, ITimeEntry>> groupsByYear = groupByPerson.GroupBy(e => e.Date.Year);
                foreach (IGrouping<int, ITimeEntry> groupByYear in groupsByYear)
                {
                    int year = groupByYear.Key;
                    var monthsDictionary = new Dictionary<string, IReadOnlyDictionary<string, float>>();
                    yearsDictionary.Add(year, monthsDictionary);

                    IEnumerable<IGrouping<string, ITimeEntry>> groupsByMonth = groupByYear.GroupBy(e => dateTimeFormatInfo.GetMonthName(e.Date.Month));
                    foreach (IGrouping<string, ITimeEntry> groupByMonth in groupsByMonth)
                    {
                        string month = groupByMonth.Key;
                        var projectsDictionary = new SortedDictionary<string, float>();
                        monthsDictionary.Add(month, projectsDictionary);

                        IEnumerable<IGrouping<string, ITimeEntry>> groupsByProjects = groupByMonth.GroupBy(e => e.Project);
                        foreach (IGrouping<string, ITimeEntry> groupByProject in groupsByProjects)
                        {
                            string project = groupByProject.Key;
                            float hours = groupByProject.Sum(e => e.Hours);
                            projectsDictionary.Add(project, hours);
                        }
                    }
                }
            }

            return personsDictionary;
        }
    }
}
