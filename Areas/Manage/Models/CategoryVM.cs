using System.ComponentModel.DataAnnotations;

namespace AspNetCoreExample.Areas.Manage.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Zorunludur")]
        public string CategoryName { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }
    }
}