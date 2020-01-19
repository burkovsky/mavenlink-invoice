using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Web.Models
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Please upload a file.")]
        public IFormFile Report { get; set; }

        public IReadOnlyDictionary<string, IReadOnlyDictionary<int, IReadOnlyDictionary<string, IReadOnlyDictionary<string, float>>>> Invoices { get; set; }
    }
}
