using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace GrafanaInNetCore.Controllers
{
    public class HomeController : Controller
    {
        Counter counter =  Metrics
           .CreateCounter("GetCityError", "این متد خطا های دریافت شهرها را نمایش می دهد");

        public IActionResult Index()
        {
            return View();
        }
    }
}
