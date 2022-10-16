namespace AspNetCoreExample.Models.ViewModels
{
    public class SepetViewModels
    {
        public int ProductId { get; set; }
        public string UrunAdi { get; set; }

        public int Adet { get; set; }
        public decimal? Fiyat { get; set; }
        public decimal? ToplamFiyat { get; set; }
    }
}