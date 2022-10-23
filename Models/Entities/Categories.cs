using System.ComponentModel.DataAnnotations;

namespace AspNetCoreExample.Models.Entities
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}