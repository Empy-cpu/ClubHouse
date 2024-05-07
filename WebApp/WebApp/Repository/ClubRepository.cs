using WebApp.Data;
using WebApp.Data.Interfaces;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Enum;


namespace WebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly WebAppDbContext _context;

        public ClubRepository(WebAppDbContext webAppDbContext)
        {
            _context = webAppDbContext;
        }

        public bool Add(Club club)
        {
            _context.Add(club);
           return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

       

        public async Task<IEnumerable<Club>> GetAll()
        {
            return  await _context.Clubs.ToListAsync();
        }

        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id); 
        }

        public async Task<Club?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).Distinct().ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
