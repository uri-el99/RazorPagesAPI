using Newtonsoft.Json;
using RazorAPI.Models;
using System.Net.Http.Headers;
using System.Text;

namespace RazorAPI.Services
{


    public class ServicesAPI: IServicesAPI
    {
        private static string _user;
        private static string _password;
        private static string _Baseurl;
        private static string _token;

        public ServicesAPI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _user = builder.GetSection("ApiSettings:user").Value;
            _password = builder.GetSection("ApiSettings:pass").Value;
            _Baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<bool> AddCar(Car carObject)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.BaseAddress = new Uri(_Baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(carObject), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Cars/NewCar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> DeleteCar(int IdCar)
        {
            bool respuesta = false;
            await GetToken();
            var client = new HttpClient();
            client.BaseAddress = new Uri(_Baseurl);
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_token}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.DeleteAsync($"api/Cars/DeleteCar/{IdCar}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<List<Car>> GetAllCars()
        {
            List<Car> carsList = new List<Car>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_Baseurl);
            var response = await client.GetAsync("api/Cars/AllCars/");//Agregar Result

            if (response.IsSuccessStatusCode)//Agregar Result después del response
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult>(jsonResult);
                carsList = result.CarsList;
            }
            return carsList;
        }

        public async Task<Car> GetCarDetails(int IdCar)
        {
            Car carObject = new Car();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_Baseurl);
            var response = await client.GetAsync($"/api/Cars/{IdCar}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult>(jsonResult);
                carObject = result.CarObject;
            }
            return carObject;
        }

        public async Task GetToken()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(_Baseurl);

            var credential = new Credentials
            {
                Username = _user,
                Password = _password
            };

            var content = new StringContent(JsonConvert.SerializeObject(credential), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Usuarios/inicioSesion", content);
            var jsonResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<credentialsResult>(jsonResult);

            _token = result.Token;

        }

        public async Task<bool> UpdateCar(Car carObject)
        {
            bool respuesta = false;

            var client = new HttpClient();
            client.BaseAddress = new Uri(_Baseurl);

            var content = new StringContent(JsonConvert.SerializeObject(carObject), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Cars/EditCar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;

        }
    }
}
