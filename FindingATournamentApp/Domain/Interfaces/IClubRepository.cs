using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FindingATournamentApp.Domain.Entities;


namespace FindingATournamentApp.Domain.Interfaces
{
    public interface IClubRepository
    {
        Task<Clube> GetById (int id);
        Task<IQueryable<Clube>> GetAll();
        
        Task<IQueryable<Clube>> GetByFilter(Clube clube);
        Task<int> Create(Clube club);
    }
}