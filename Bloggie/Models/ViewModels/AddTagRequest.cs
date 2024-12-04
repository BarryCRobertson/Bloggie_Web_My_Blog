using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Webb.Models.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? DisplayName { get; set; }
    }
}
