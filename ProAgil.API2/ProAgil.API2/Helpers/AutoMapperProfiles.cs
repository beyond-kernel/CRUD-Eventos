using AutoMapper;
using Model;
using ProAgil.API2.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API2.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //associacoes de muitos para muitos para o automapper
            CreateMap<Evento, EventoDto>().ForMember(x => x.Palestrantes, opt => { opt.MapFrom(src => src.PalestranteEvento.Select(s => s.Palestrante).ToList()); });
            CreateMap<EventoDto, Evento>();
            CreateMap<Palestrante, PalestranteDto>().ForMember(x => x.Eventos, opt => { opt.MapFrom(src => src.PalestranteEvento.Select(s => s.Evento).ToList()); });
            CreateMap<PalestranteDto, Palestrante>();
            CreateMap<Lote, LoteDto>();
            CreateMap<LoteDto, Lote>();
            CreateMap<RedeSocial, RedeSocialDto>();
            CreateMap<RedeSocialDto, RedeSocial>();
        }
    }
}
