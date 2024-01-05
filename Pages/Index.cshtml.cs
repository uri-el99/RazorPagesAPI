using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorAPI.Models;

namespace RazorAPI.Pages
{
    public class IndexModel : PageModel
    {
        public List<Car> carsList = new List<Car>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Car car1 = new Car();
            car1.Id = 1;
            car1.Make = "Toyota";
            car1.Model = "Corolla";
            car1.Color = "Red";
            car1.Year = 2019;
            car1.Doors = 4;
            carsList.Add(car1);
        }
    }
}