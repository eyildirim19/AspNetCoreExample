using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreExample.Areas.Manage.Controllers
{
	// Arae içeriisndeki controllerlara Arae attribute ile name verilir

	[Area("Manage")]
	public class HomeController : Controller
	{
		public string Index()
		{
			return "ben yönetim sayfasının indexiyim...";
		}
	}
}
