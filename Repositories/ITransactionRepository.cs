using CarRentalSystem.Models;
namespace CarRentalSystem.Repositories
{
    public interface ITransactionRepository
    {
        public Task AddTransaction(Transaction transaction);
        public Task<List<Transaction>> GetBookingHistory(int userId);
    }
}
