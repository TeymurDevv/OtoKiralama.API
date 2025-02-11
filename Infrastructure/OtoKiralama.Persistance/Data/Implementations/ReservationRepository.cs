    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private readonly AppDbContext _context;
        public ReservationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<Reservation>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<Reservation, bool>> predicate)
        {
            return await _context.Set<Reservation>().CountAsync(predicate);
        }

        public async Task<int> GetLastReservationNumberForYear(int year)
        {
            var lastReservation = await _context.Reservations
                .Where(r => r.ReservationNumber.StartsWith(year.ToString()))
                .OrderByDescending(r => r.ReservationNumber)
                .FirstOrDefaultAsync();

            if (lastReservation == null)
                return 0;

            return int.Parse(lastReservation.ReservationNumber.Split('-')[1]);
        }
    }
}
