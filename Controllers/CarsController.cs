using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            this._carService = carService;
        }


        //Rent Car
        [HttpPost("rent")]
        [Authorize(Roles ="User,Admin")]
        public async Task<IActionResult>RentCar(RentDTO rent)
        {
            int userId = GetUserIdFromClaims();
            try
            {
                var result = await _carService.RentCar(userId, rent.CarId, rent.days);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }         
        }

        //get user booking history
        [HttpGet("BookingHistory")]
        [Authorize(Roles ="Admin,User")]
        public async Task<IActionResult> GetBookingHistory()
        {
            int userId = GetUserIdFromClaims();
            var history=await _carService.getBookingHistory(userId);
            if(history == null || !history.Any())
            {
                return NotFound("No booking history found");
            }
            return Ok(history);
        }

        //Get all available cars
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars()
        {
            var cars = await _carService.getAllAvailableCars();
            return Ok(cars);
        }


        //Get car by its id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car=await _carService.getCarById(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }
            return Ok(car);
        }


        //Add car by admin 
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedCar=await _carService.AddCar(car);
            return CreatedAtAction(nameof(AddCar), new {id=car.Id},car);
        }

        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedcar = await _carService.UpdateCar(car);
                return Ok(car);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Delete car by admin
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
           var success= await _carService.DeleteCar(id);
            if (!success)
            {
                return NotFound($"Car with ID {id} not found.");
            }
            return NoContent();

        }

        //Helper method to get userId from Claims
        private int GetUserIdFromClaims()
        {
            var userClaims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId");
            if (userClaims == null)
            {
                throw new UnauthorizedAccessException("Invalid User");
            }
            return int.Parse(userClaims.Value);
        }


    }
}
