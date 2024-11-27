using CarRentalSystem.Models;
using CarRentalSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class TransactionRepository:ITransactionRepository
    {
        private readonly CarRentalDbContext _context;
        public TransactionRepository(CarRentalDbContext context) {
            _context=context;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        //to find user booking history
        public async Task<List<Transaction>> GetBookingHistory(int userId)
        {
            return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();

        }
    }
}
