namespace RazorAPI.Models
{
    public class ApiResult
    {
        public string Message { get; set; }
        public List<Car> CarsList { get; set; }
        public Car CarObject { get; set; }
    }
}
