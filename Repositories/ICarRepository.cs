using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ICarRepository
    {
        public Task<IEnumerable<Car>> GetAvailableCars();
        public Task<Car> GetCarById(int id);
        public Task<Car> AddCar(Car car);    
        public Task<Car> UpdateCar(Car car);

        public Task<bool> DeleteCar(int id);
        

    }
}
