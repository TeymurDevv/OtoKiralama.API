using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class ReservationRepository:Repository<Reservation>,IReservationRepository
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
    }
}
