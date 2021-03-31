using System.Collections.Generic;

using Microsoft.AspNetCore.Http;

namespace Abstractions
{
    public interface IReportHandler
    {
        IReadOnlyList<ITimeEntry> ProcessReport(IFormFile file, out string message);
    }
}
