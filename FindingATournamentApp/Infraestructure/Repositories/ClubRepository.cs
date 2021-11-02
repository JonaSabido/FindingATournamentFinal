using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindingATournamentApp.Domain.Entities;
using FindingATournamentApp.Infraestructure.Data;

namespace FindingATournamentApp.Infraestructure.Repositories
{
    public class ClubRepository
    {

        private readonly FindingATournamentContext _context;

        public ClubRepository()
        {
            _context = new FindingATournamentContext();

        }
    
        public IEnumerable<Clube> GetAll()
        {
            var query = _context.Clubes.Select(clube => clube);
            return query;
        }

        public IEnumerable<Clube> GetContains(string word)
        {
            var query = _context.Clubes.Where( x => x.ClubName.Contains(word));
            return query;
        }

        public IEnumerable<Clube> GetByPhone(string number)
        {
            var query = _context.Clubes.Where( x => x.ClubContactNumber == number);
            return query;
        }

        /*
        public IEnumerable<Clube> GetByFiltersClub(Clube clubes)
        {
            var query = _context.Clubes.Select(x => x);
            if(!string.IsNullOrEmpty(clubes.ClubName))
                query = query.Where(x => x.ClubName.Contains(clubes.ClubName));
        
            if (!string.IsNullOrEmpty(clubes.ClubContactNumber))
                query = query.Where(x => x.ClubContactNumber.Contains(clubes.ClubContactNumber));
            return query;
        }
        */

    }
}