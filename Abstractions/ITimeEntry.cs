using System;

namespace Abstractions
{
    public interface ITimeEntry
    {
        DateTime Date { get; }

        string Person { get; }

        string Project { get; }

        string Task { get; }

        string Notes { get; }

        string Location { get; }

        string Status { get; }

        float Hours { get; }

        int Minutes { get; }
    }
}
