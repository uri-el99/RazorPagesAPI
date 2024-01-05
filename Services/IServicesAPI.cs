using RazorAPI.Models;

namespace RazorAPI.Services
{
    public interface IServicesAPI
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetCarDetails(int IdCar);
        Task<bool> AddCar(Car carObject);
        Task<bool> UpdateCar(Car carObject);
        Task<bool> DeleteCar(int IdCar);


    }
}
