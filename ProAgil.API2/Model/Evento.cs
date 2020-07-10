using System;
using System.Collections.Generic;

namespace Model
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public List<Lote> Lotes { get; set; }
        public string Telefone { get; set; }
        public string ImagemUrl { get; set; }
        public string Email { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
        public List<PalestranteEvento> PalestranteEvento { get; set; }

    }
}
