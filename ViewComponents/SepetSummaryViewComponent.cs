using AspNetCoreExample.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspNetCoreExample.ViewComponents
{
    public class SepetSummaryViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            string? sepetstrList = HttpContext.Session.GetString("sepet");

            List<Sepet> sepetList;
            if (sepetstrList != null)
                sepetList = JsonConvert.DeserializeObject<List<Sepet>>(sepetstrList);
            else
                sepetList = new List<Sepet>();

            return View(sepetList);
        }
    }
}