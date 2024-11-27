using CarRentalSystem.Models;
namespace CarRentalSystem.Services

{
    public interface ICarService
    {

        public Task<string> RentCar(int userId, int carId, int days);
        public Task<bool> IsCarAvailable(int CarId);
        public Task<decimal> CalculateRentalPrice(int carId, int rentalDays);

        public Task<List<Transaction>> getBookingHistory(int userId);
        public Task<IEnumerable<Car>> getAllAvailableCars();
        public Task<Car> getCarById(int id);
        public Task<Car> AddCar(Car car);
        public Task<Car> UpdateCar(Car car);
        public Task<bool> DeleteCar(int id);
    }
}
