using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProAgilRepository
    {
        //general
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        //Events
        Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePlaestrantes);
        Task<Evento[]> GetAllEventosAsync(bool includePlaestrantes);
        Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePlaestrantes);

        //Palestrants
        Task<Palestrante[]> GetAllPalestratesAsyncByName(string tema, bool includePlaestrantes);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includePlaestrantes);
        Task<Palestrante> GetAllPalestranteAsyncById(int EventoId, bool includePlaestrantes);

    }
}
