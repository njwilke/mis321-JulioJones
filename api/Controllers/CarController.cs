using api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        // GET: api/<CarController>
        [HttpGet]

        public List<Car> Get() 
        {
            CarUtility utility = new CarUtility();
            return utility.GetAllCars();
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]

        public Car Get(int id)
        {
            CarUtility utility = new CarUtility();
            List<Car> myCars = utility.GetAllCars();
            foreach(Car car in myCars) {
                if(car.CarID == id) {
                    return car;
                }
            }
            return new Car();
        }

        // POST api/<CarController>
        [HttpPost]

        public void Post(Car myCar)
        {
            CarUtility utility = new CarUtility();
            System.Console.WriteLine($"{myCar.CarID} {myCar.CarMakeModel} {myCar.Mileage} {myCar.Date} {myCar.Hold} {myCar.Sold}");
            utility.AddCar(myCar);
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}")]

        public void Put(int id, [FromBody] Car myCar)
        {
            CarUtility utility = new CarUtility();
            utility.UpdateHold(myCar);
        }

        [HttpPut]

        public void Put([FromBody] Car myCar)
        {
            CarUtility utility = new CarUtility();
            System.Console.WriteLine($"{myCar.Sold} {myCar.CarID}");
            utility.UpdateSold(myCar);
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
