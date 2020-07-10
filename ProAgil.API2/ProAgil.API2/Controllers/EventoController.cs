using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Repository;

namespace ProAgil.API2.Controllers
{
    [Route("site/Evento")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        // private readonly DataContext _dataContext;
        private readonly IProAgilRepository _respository;

        public EventoController(IProAgilRepository respository)
        {
            _respository = respository;
        }

        // GET: api/Evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //var result = new List<Evento>() { new Evento() { EventoId = 1, Tema = "Angular e .NET Core", Local = "SP", Lote = "1º", QtdPessoas = 250, DataEvento = "21/12/2019" } };

                //return Ok(result);

                var result = await _respository.GetAllEventosAsync(true);

                return Ok(result);

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

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }

        // GET: api/Evento/EventoId
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _respository.Add(model);

                if(await _respository.SaveChangesAsync())
                { 
                   return Created($"/api/Evento/{model.EventoId}",model); 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }

            return BadRequest();
        }

  // GET: api/Evento/EventoId
        [HttpPut]
        public async Task<IActionResult> Put(Evento model)
        {
            try
            {

                var evento = await _respository.GetAllEventoAsyncById(model.EventoId, false);
                if(evento == null) return NotFound(); 

                _respository.Update(model);

                if(await _respository.SaveChangesAsync())
                { 
                   return Created($"/api/Evento/{model.EventoId}",model); 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }

            return BadRequest();
        }


        [HttpDelete]
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

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }
    }
}
