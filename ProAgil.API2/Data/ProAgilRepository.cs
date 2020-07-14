using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        protected DataContext _dataContext;

        public ProAgilRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            //NoTracking Para não travar os recursos nas querys
            _dataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        } 

        public void Add<T>(T entity) where T : class
        {
            _dataContext.AddAsync(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _dataContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePlaestrantes = false)
        {
            IQueryable<Evento> query = _dataContext.Eventos.Include(x => x.Lotes).Include(x => x.RedesSociais);

            if (includePlaestrantes)
            {
                query = query.Include(x => x.PalestranteEvento).ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderByDescending(x => x.DataEvento);

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePlaestrantes)
        {
            IQueryable<Evento> query = _dataContext.Eventos.Include(x => x.Lotes).Include(x => x.RedesSociais);

            if (includePlaestrantes)
            {
                query = query.Include(x => x.PalestranteEvento).ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderByDescending(x => x.DataEvento).Where(x => x.EventoId == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePlaestrantes)
        {
            IQueryable<Evento> query = _dataContext.Eventos.Include(x => x.Lotes).Include(x => x.RedesSociais);

            if (includePlaestrantes)
            {
                query = query.Include(x => x.PalestranteEvento).
                        ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderByDescending(x => x.DataEvento).Where(x => x.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteAsyncById(int PalestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _dataContext.Palestrantes.Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(x => x.PalestranteEvento).ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(x => x.Nome).Where(x => x.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
           IQueryable<Palestrante> query = _dataContext.Palestrantes.Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(x => x.PalestranteEvento).ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(x => x.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestratesAsyncByName(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _dataContext.Palestrantes.Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(x => x.PalestranteEvento).ThenInclude(pe => pe.Evento);
            }

            query = query.Where(x => x.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dataContext.SaveChangesAsync()) > 0;
        }

    }
}
