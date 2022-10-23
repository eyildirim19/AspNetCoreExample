namespace AspNetCoreExample.Models.ViewModels
{
	public class PaymentVM
	{
		public string Name { get; set; }
		public string CreditCartNumber { get; set; }
		public int CcMonth { get; set; }
		public int CcYear { get; set; }
		public string CVV { get; set; }
	}
}
