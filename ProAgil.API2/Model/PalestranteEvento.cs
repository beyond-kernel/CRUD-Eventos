using System;

namespace Model
{
    public class PalestranteEvento
    {
        public int PalestranteEventoId { get; set; }
        public Palestrante Palestrante { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}