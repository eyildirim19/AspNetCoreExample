using System.ComponentModel.DataAnnotations;

namespace AspNetCoreExample.Models.Entities
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public int? CategoryId { get; set; }
    }
}