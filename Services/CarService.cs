using System.Diagnostics;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public class CarService:ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly CarRentalDbContext _carRentalDbContext;
        private readonly ITransactionRepository _transactionRepository;
        private readonly SmsService _smsService;
        private readonly IUserRepository _userRepository;

        //Constructor
        public CarService(ICarRepository carRepository,CarRentalDbContext carRentalDbContext, 
            ITransactionRepository transactionRepository, SmsService smsService,IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _carRentalDbContext = carRentalDbContext;
            _carRepository = carRepository;
            _smsService = smsService;
            _userRepository= userRepository;
        }

        //To rent a car
        public async Task<String> RentCar(int userId, int carId, int days)
        {
            using (var transaction = await _carRentalDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var car = await _carRepository.GetCarById(carId);
                    //checking is car is found
                    if (car == null)
                    {
                        throw new Exception("car not Found");
                    }

                    //check car availability
                    if (!car.isAvailable)
                    {
                        throw new Exception("Car not Available.");
                    }

                    //calculating price
                    decimal totalPrice = await CalculateRentalPrice(carId, days);
                    car.isAvailable = false;
                    await _carRepository.UpdateCar(car);

                    //adding transaction entry
                    var transactionRecord = new Transaction
                    {
                        UserId = userId,
                        CarId = carId,
                        RentDays = days,
                        TotalAmount = totalPrice,
                        RentDate = DateTime.Now,
                    };
                    await _transactionRepository.AddTransaction(transactionRecord);
                    await transaction.CommitAsync();

                    //fetching user by id
                    var user=await _userRepository.GetUserById(userId);
                    if (user != null)
                    {
                        //sending sms
                        var suceed=await _smsService.SendSms(user.PhoneNumber, user.Name, car.Make, days, totalPrice);
                        if (!suceed)
                        {
                            throw new ArgumentException($"The phone number {user.PhoneNumber} is unverified");
                        }
                    }


                    return $"Car  has been rented by {userId} for {days} day(s). Total Price: ${totalPrice}";
                }
                catch(ArgumentException ex)
                {
                    throw new Exception($"SMS not sent:{ex.Message}");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"{ex.Message}");
                }
            }
            }

        //calculate rentPrice
        public async Task<decimal> CalculateRentalPrice(int carId, int rentalDays)
        {
            var car = await _carRepository.GetCarById(carId);
            if (car == null)
            {
                return 0;
            }
            return car.PricePerDay * rentalDays;
        }

        public async Task<bool> IsCarAvailable(int CarId)
        {
            var car=await _carRepository.GetCarById(CarId);
            return car.isAvailable;
        }

        //get booking history
        public async Task<List<Transaction>> getBookingHistory(int userId)
        {
            return await _transactionRepository.GetBookingHistory(userId);
        }

        public async Task<IEnumerable<Car>> getAllAvailableCars()
        {
            return await _carRepository.GetAvailableCars();

        }

        //Get car by id
        public async Task<Car> getCarById(int id)
        {
            return await _carRepository.GetCarById(id);
        }

        //add car
        public async Task<Car> AddCar(Car car)
        {
            return await _carRepository.AddCar(car);
        }

        //update car details
        public async Task<Car> UpdateCar(Car car)
        {
            return await _carRepository.UpdateCar(car);
        }

        //delete car details
        public async Task<bool> DeleteCar(int id)
        {
            return await _carRepository.DeleteCar(id);
        }
    }
}
