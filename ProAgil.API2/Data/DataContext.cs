using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public Microsoft.EntityFrameworkCore.DbSet<Evento> Eventos { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Palestrante> Palestrantes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PalestranteEvento> PalestranteEventos { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Lote> Lotes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<RedeSocial> RedeSociais { get; set; }

    }
}
