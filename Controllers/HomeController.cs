using Microsoft.AspNetCore.Mvc;
using centrala.Models;

namespace centrala.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Kontakt()
    {
        return View();
    }

    public IActionResult ElektrarnaTed()
    {
        ViewBag.data = GlobalValues.latestSensors;
        return View();
    }

    public IActionResult ElektrarnaZaznam(string? d = null, int? o = null, bool? log = null)
    {
        DateTime date;
        int offset;

        ViewBag.validDate = ConverterValidator.convertValidateDate(d, out date);    
        ViewBag.validOffset = ConverterValidator.convertValidateOffset(o, out offset);

        if (!ViewBag.validDate) offset = 0;

        if (log == true) ViewBag.chartType = "logarithmic";
        else ViewBag.chartType = "linear";

        var eleZaznam = new ElektrarnaZaznam();
        ViewBag.date = date.AddDays(offset).ToString("yyyy-MM-dd");
        ViewBag.CSV = eleZaznam.GetData(ViewBag.date);
        ViewBag.battery = eleZaznam.GetBatterySOC()!.data;
        ViewBag.validCSV = !ConverterValidator.isEmpty(ViewBag.CSV);

        return View();
    }
}
