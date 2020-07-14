using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using ProAgil.API2.Dtos;
using Repository;

namespace ProAgil.API2.Controllers
{
    [Route("site/Evento")]
    [ApiController] //utilizado no dotnet core substituindo o frombody
    public class EventoController : ControllerBase
    {
        // private readonly DataContext _dataContext;
        private readonly IProAgilRepository _respository;
        private readonly IMapper _mapper;

        public EventoController(IProAgilRepository respository, IMapper mapper)
        {
            _respository = respository;
            _mapper = mapper;
        }

        // GET: api/Evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //var result = new List<Evento>() { new Evento() { EventoId = 1, Tema = "Angular e .NET Core", Local = "SP", Lote = "1º", QtdPessoas = 250, DataEvento = "21/12/2019" } };

                //return Ok(result);

                var eventos = await _respository.GetAllEventosAsync(true);

                var finalResults = _mapper.Map<List<EventoDto>>(eventos);

                return Ok(finalResults);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }


       // GET: api/Evento/EventoId
        [HttpGet("getById/{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var result = await _respository.GetAllEventoAsyncById(EventoId, true);

                var finalResults = _mapper.Map<EventoDto>(result);


                return Ok(finalResults);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }

        // GET: api/Evento/EventoId
        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                
                _respository.Add(evento);

                if(await _respository.SaveChangesAsync())
                { 
                   return Created($"/api/Evento/{model.EventoId}",_mapper.Map<EventoDto>(evento)); 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }

            return BadRequest();
        }

  // GET: api/Evento/EventoId
        [HttpPut("put/{EventoId}")]
        public async Task<IActionResult> Put(EventoDto model)
        {
            try
            {

                var evento = await _respository.GetAllEventoAsyncById(model.EventoId, false);
                if(evento == null) return NotFound(); 

                _mapper.Map(model, evento);

                _respository.Update(evento);

                if(await _respository.SaveChangesAsync())
                { 
                   return Created($"/api/Evento/{model.EventoId}",_mapper.Map<EventoDto>(evento)); 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }

            return BadRequest();
        }


        [HttpDelete("Delete/{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId){
        
        try
        {
            var evento = await _respository.GetAllEventoAsyncById(EventoId, false);
                if(evento == null) return NotFound(); 

            _respository.Delete(evento);    

            if(await _respository.SaveChangesAsync()){
                 return  Ok();     
            }  
        }
            catch(Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }  

            return BadRequest();
        }

           // GET: api/Evento/EventoId
        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var result = await _respository.GetAllEventosAsyncByTema(tema, true);

                var evento = _mapper.Map<List<Evento>>(result);

                return Ok(evento);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }
    }
}
