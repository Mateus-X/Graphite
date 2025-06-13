using System.ComponentModel.DataAnnotations;

namespace Graphite.Source.Domain.Models
{
    public class DataframeModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
