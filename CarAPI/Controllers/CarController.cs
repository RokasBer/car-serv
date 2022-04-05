using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
                    Year = 1999,
                    Engine = 2.5,
                    Price = 3000
                },
                new Car
                {
                    Id = 2,
                    Manufacturer= "MERCEDES",
                    Model = "BENZ W124",
                    Year = 1989,
                    Engine = 2.4,
                    Price = 2600
                },
                new Car
                {
                    Id = 3,
                    Manufacturer= "TOYOTA",
                    Model = "SUPRA MK4",
                    Year = 1997,
                    Engine = 3.2,
                    Price = 5000
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
            if (CarExists(car.Id))
            {
                return BadRequest("Car with same id already exists");
            }

            cars.Add(car);
            return Ok(cars);
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
            
            return Ok(cars);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<List<Car>>> UpdatePatch(int id, [FromBody]JsonPatchDocument<Car> doc)
        {
            var car = cars.Find(h => h.Id == id);
            if (car == null)
            {
                return BadRequest("Car not found.");
            }
            doc.ApplyTo(car, ModelState);
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


        private bool CarExists(int id)
        {
            return cars.Any(e => e.Id == id);
        }


    }
}
