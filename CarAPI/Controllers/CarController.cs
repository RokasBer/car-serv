using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private static List<Car> cars = new List<Car>
            {
                new Car {
                    Id = 1,
                    Manufacturer= "BMW",
                    Model = "E36",
                    Year = 1999
                },
                new Car
                {
                    Id = 2,
                    Manufacturer= "MERCEDES",
                    Model = "BENZ W124",
                    Year = 1989
                },
                new Car
                {
                    Id = 3,
                    Manufacturer= "TOYOTA",
                    Model = "SUPRA MK4",
                    Year = 1997
                }
            };


        [HttpGet]
        public async Task<ActionResult<List<Car>>> Get()
        {
            
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = cars.Find(h => h.Id == id);
            if(car == null)
            {
                return BadRequest("Car not found.");
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<List<Car>>> AddCar(Car car)
        {
            cars.Add(car);
            return Ok(cars);
        }

        [HttpPut]
        public async Task<ActionResult<List<Car>>> UpdateCar(Car request)
        {
            var car = cars.Find(h => h.Id == request.Id);
            if (car == null)
            {
                return BadRequest("Car not found.");
            }

            car.Manufacturer = request.Manufacturer;
            car.Model = request.Model;
            car.Year = request.Year;
            
            return Ok(cars);
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
            return Ok(cars);
        }



    }
}
