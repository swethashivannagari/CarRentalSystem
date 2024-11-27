using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarRepository:ICarRepository
    {
        private readonly CarRentalDbContext _context;
        public CarRepository(CarRentalDbContext context) {
        _context = context;
        }

        //to get available cars
        public async Task<IEnumerable<Car>> GetAvailableCars()
        {
            return await _context.Cars.Where(c=>c.isAvailable).ToListAsync();
        }

        //get car by id
        public async Task<Car> GetCarById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        //Add new car
        public async Task<Car> AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;

        }

        //Update car details
        public async Task<Car> UpdateCar(Car updatedCar)
        {
            var existingCar=await _context.Cars.FirstOrDefaultAsync(c=>c.Id==updatedCar.Id);
            if (existingCar != null)
            {
                // Modify the properties without editing id
                existingCar.Make = updatedCar.Make;
                existingCar.Model = updatedCar.Model;
                existingCar.Year = updatedCar.Year;
                existingCar.PricePerDay = updatedCar.PricePerDay;
                existingCar.isAvailable = updatedCar.isAvailable;
                await _context.SaveChangesAsync();
                return updatedCar;
            }
            throw new Exception($"Car with Id {updatedCar.Id} not found");
        }

        //delete a car
        public async Task<bool> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return false;
               
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
