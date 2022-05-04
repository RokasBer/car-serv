using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private static int idCounter = 4;
        private readonly string path = "http://contacts:5000/contacts/";

        private static List<Car> cars = new List<Car>
            {
                new Car {
                    Id = 1,
                    Manufacturer= "BMW",
                    Model = "E36",
                    Year = 1999,
                    Engine = 2.5,
                    Price = 600,
                    isRented = true,
                    renterId = 87014

                },
                new Car
                {
                    Id = 2,
                    Manufacturer= "MERCEDES",
                    Model = "BENZ W124",
                    Year = 1989,
                    Engine = 2.4,
                    Price = 500,
                    isRented = true,
                    renterId = 12345
    },
                new Car
                {
                    Id = 3,
                    Manufacturer= "TOYOTA",
                    Model = "SUPRA MK4",
                    Year = 1997,
                    Engine = 3.2,
                    Price = 800,
                    isRented = false
                }
            };


        [HttpGet]
        public async Task<ActionResult<List<Car>>> Get()
        {

            return Ok(cars);
        }


        [HttpGet("{id}/user")]
        public async Task<ActionResult<dynamic>> GetCarUser(int id)
        {
            var car = cars.Find(h => h.Id == id);

            if(car == null)
            {
                return BadRequest("Car not found");
            }    
            if(car.renterId == null)
            {
                return NotFound("Car with this id isn't rented");
            }
            Renter renter = null;
            try
            {
                using(var httpClient = new HttpClient())
                {
                    using(var response = await httpClient.GetAsync(path + car.renterId))
                    {
                        string APIResponse = await response.Content.ReadAsStringAsync();
                        renter = JsonConvert.DeserializeObject<Renter>(APIResponse);
                    }
                }
            }
            catch(Exception ex)
            { return BadRequest(ex.Message); }

            renter.Id = (long)car.renterId;
            CarWithRenter carWithRenter = new CarWithRenter(car);
            carWithRenter.renter = renter;
            return carWithRenter;

        }

        [HttpGet("user/{isRented?}")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetCarWithUser(bool isRented = true)
        {
            if (isRented)
            {

                var allCars = cars;
                List<Object> carUsers = new List<Object>();
                foreach (var car in allCars)
                {
                    Renter renter = null;
                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync(path + car.renterId))
                            {
                                string APIResponse = await response.Content.ReadAsStringAsync();
                                renter = JsonConvert.DeserializeObject<Renter>(APIResponse);

                            }
                        }
                        CarWithRenter carWithRenter = new CarWithRenter(car);
                        carWithRenter.renter = renter;
                        carUsers.Add(carWithRenter);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        carUsers.Add(car);
                    }
                }
                return carUsers;
            }
            else
            {
                return cars;
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = cars.Find(h => h.Id == id);
            if(car == null)
            {
                return BadRequest("Car not found.");
            }
            return car;
        }

        [HttpPost]
        public async Task<ActionResult<List<Car>>> AddCar(Car car)
        {
            if(car.Model == string.Empty)
            {
                return BadRequest("No model");
            }
            if(car.Manufacturer == string.Empty)
            {
                return BadRequest("No manufacturer");
            }
            if(car.Year == null || car.Year == 0)
            {
                return BadRequest("No year");
            }
            if(!car.isRented)
            {
                car.renterId = null;
            }

            
            for(int i = 1; i <= CarController.idCounter; i++)
            {
                var temp = cars.Find(h => h.Id == i);
                if(temp == null)
                {
                    car.Id = i;
                    break;
                }
            }
            
            CarController.idCounter++;

            cars.Add(car);

            HttpContext.Response.Headers["Location"] = "https://localhost:7051/api/Car/" + car.Id;
            //return CreatedAtAction(nameof(AddCar), new { id = car.Id }, car);
            return Ok(car);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Car>>> UpdateCar(int id, [FromBody]Car request)
        {
            var car = cars.Find(h => h.Id == id);
            if (car == null)
            {
                return BadRequest("Car not found.");
            }
            if(request.Id == 0)
            {
                request.Id = id;
            }    
            if(request.Id != id)
            {
                return BadRequest("Id does not match");
            }
            car.Manufacturer = request.Manufacturer;
            car.Model = request.Model;
            car.Year = request.Year;
            car.Engine = request.Engine;
            car.Price = request.Price;
            car.isRented = request.isRented;
            if(!car.isRented)
            {
                car.renterId = null;
            }
            
            return Ok(cars);
        }

        /*[HttpPut("{id}/user")]
        public async Task<IActionResult> UpdateCarUser(int id, Renter user)
        {
            var car = cars.Find(h => h.Id == id);
            if (car == null)
            {
                return BadRequest("Car not found.");
            }

            int userId = car.Id;
           
        }*/


        [HttpPatch("{id}")]
        public async Task<ActionResult<List<Car>>> UpdatePatch(int id, [FromBody]Car request)
        {
            var car = cars.Find(h => h.Id == id);
            if (car == null)
            {
                return BadRequest("Car not found.");
            }

            if (request.Manufacturer != string.Empty)
            {
                car.Manufacturer = request.Manufacturer;
            }
            if (request.Model != string.Empty)
            {
                car.Model = request.Model;
            }
            if (request.Year != null)
            {
                car.Year = request.Year;
            }
            if (request.Engine != null)
            {
                car.Engine = request.Engine;
            }
            if (request.Price != null)
            {
                car.Price = request.Price;
            }
            if(request.isRented != null)
            {
                car.isRented = request.isRented;
            }
            if(request.isRented == null)
            {
                car.renterId = null;
            }    
           

            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Car>>> DeleteCar(int id)
        {
            var car = cars.Find(h => h.Id == id);
            if (car == null)
            {
                return BadRequest("Car not found.");
            }
            cars.Remove(car);
            return NoContent();
        }

        [HttpDelete("{id}/user")]
        public async Task<IActionResult> DeleteCarUser(int id)
        {
            var car = cars.Find(h => h.Id == id);

            if (car == null)
            {
                return BadRequest("Car not found");
            }
            if (car.renterId == null)
            {
                return NotFound("Car with this id isn't rented");
            }
            int renterId = (int)car.renterId;
            using (var httpClient = new HttpClient())
            {
                using(var response = await httpClient.DeleteAsync(path + renterId))
                {
                    string APIResponse = await response.Content.ReadAsStringAsync();
                }
            }
            foreach (var carUser in cars.Where(e => e.renterId == renterId))
            {
                carUser.renterId = null;
                carUser.isRented = false;

            }


            return NoContent();
        }


        private bool CarExists(int id)
        {
            return cars.Any(e => e.Id == id);
        }


    }
}
