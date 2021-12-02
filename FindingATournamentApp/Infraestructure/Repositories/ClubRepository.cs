using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindingATournamentApp.Domain.Entities;
using FindingATournamentApp.Domain.Interfaces;
using FindingATournamentApp.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

// Universidad Tecnológica Metropolitana
// Aplicaciones Web Orientada a Servicios
// Maestro: Joel Ivan Chuc UC
// Actividad: Solucion Tecnica del Proyecto
// Integrantes del equipo: Balam Rosas Christian Jesús, Herrera Caro Abraham Enrique, Sabido Reynoso Jonathan Missael
// 4C
// Parcial 3
// Entrega: 01/12/2021

namespace FindingATournamentApp.Infraestructure.Repositories
{
    public class ClubRepository : IClubRepository
    {

        private readonly FindingATournamentContext _context;

        public ClubRepository(FindingATournamentContext context)
        {
            this._context = context;

        }

    
        public async Task<IQueryable<Clube>> GetAll()
        {
            //Origen|Colección Método Iterador
            var query = await _context.Clubes.AsQueryable<Clube>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<Clube> GetById(int id)
        {            
            var query = await _context.Clubes.FirstOrDefaultAsync(x => x.Id == id);
            return query;


        }

        public bool Exist(Expression<Func<Clube, bool>> expression)
        {
            return _context.Clubes.Any(expression);
        }
        public async Task<IQueryable<Clube>> GetByFilter(Clube clube)
        {
            if(clube == null)
                return new List<Clube>().AsQueryable();

            var query = _context.Clubes.AsQueryable();

            if(!string.IsNullOrEmpty(clube.ClubName))
                query = query.Where(x => x.ClubName.Contains(clube.ClubName));

            if(!string.IsNullOrEmpty(clube.ClubAddress))
                query = query.Where(x => x.ClubAddress.Contains(clube.ClubAddress));

            if(!string.IsNullOrEmpty(clube.ClubContactNumber))
                query = query.Where(x => x.ClubContactNumber == clube.ClubContactNumber);

            if(clube.ClubLatitude >= 0)
                query = query.Where(x => x.ClubLatitude == clube.ClubLatitude);

            if(clube.ClubLength >= 0)
                query = query.Where(x => x.ClubLength == clube.ClubLength);

            if(!string.IsNullOrEmpty(clube.ClubSchedule))
                query = query.Where(x => x.ClubSchedule.Contains(clube.ClubSchedule));

            var result = await query.ToListAsync();

            return result.AsQueryable().AsNoTracking();
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


        public async Task<int> Create(Clube club){
            
            var entity = club;
            await _context.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows<= 0){
                throw new Exception("No se pudo realizar el registro");
            }

            return entity.Id;
        }

        public async Task<bool> Update(int id,  Clube club)
        {
            if(id <= 0 || club == null)
                throw new ArgumentException("Falta información para continuar con el proceso de modificación...");

            var entity = await GetById(id);

            entity.ClubName = club.ClubName;
            entity.ClubAddress = club.ClubAddress;
            entity.ClubContactNumber = club.ClubContactNumber;
            entity.ClubLatitude = club.ClubLatitude;
            entity.ClubLength = club.ClubLength;
            entity.ClubSchedule = club.ClubSchedule;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}