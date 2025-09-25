using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace MVC.Controllers
{
    public class LabController: Controller
    {
        public IActionResult Info()
        {
            var LabData = new
            {
                Number = 1,
                Topic = "Вступ до ASP.NET Core",
                Purpose = "Ознайомитися з основними принципами роботи .NET," +
                " навчитися налаштовувати середовище розробки та встановлювати необхідні компоненти," +
                " набути навичок створення рішень та проектів різних типів, набути навичок обробки запитів з використанням middleware.",
                Student = "Зарембицький Єгор",
            };
            return View(LabData);
        }
    }
}
