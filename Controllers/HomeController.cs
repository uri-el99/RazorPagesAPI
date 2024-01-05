using Abp.Web.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using RazorAPI.Models;
using RazorAPI.Services;

namespace RazorAPI.Controllers
{
    public class HomeController : Controller
    {
        public List<Car> carsList = new List<Car>();

        private IServicesAPI _servicesAPI;

        public HomeController(IServicesAPI servicesAPI)
        {
            _servicesAPI = servicesAPI;
        }

        public async Task<IActionResult> Index()
        {
            //List<Car> carsList = await _servicesAPI.GetAllCars();
            Car car1 = new Car();
            car1.Id = 1;
            car1.Make = "Toyota";
            car1.Model = "Corolla";
            car1.Color = "Red";
            car1.Year = 2019;
            car1.Doors = 4;
            carsList.Add(car1);
            return View(carsList);
        }

        public async Task<IActionResult> CarProduct(int IdCar)
        {
            Car carModel = new Car();

            ViewBag.Action = "Add";
            if (IdCar != 0)
            {
                ViewBag.Action = "Edit";
                carModel = await _servicesAPI.GetCarDetails(IdCar);
            }
            return View(carModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCHanges(Car carModel)
        {
            bool respuesta;

            if (carModel.Id == 0)
            {
                respuesta = await _servicesAPI.AddCar(carModel);
            }
            else
            {
                respuesta = await _servicesAPI.UpdateCar(carModel);
            }
            
            if (respuesta) return RedirectToAction("Index");
            else return NoContent();

        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
