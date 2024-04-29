using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Interfaces;
using WebApp.Models;

namespace WebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly WebAppDbContext _context;

        public RaceRepository(WebAppDbContext webAppDbContext)
        {
            this._context = webAppDbContext;
        }

        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }


        public async Task<IEnumerable<Race>> GetAll()
        {
            return (IEnumerable<Race>)await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            return (IEnumerable<Race>)await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

      

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
