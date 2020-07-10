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
    [Route("site/Palestrante")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        // private readonly DataContext _dataContext;
        private readonly IProAgilRepository _respository;

        public PalestranteController(IProAgilRepository respository)
        {
            _respository = respository;
        }

        // GET: api/Palestrante
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _respository.GetAllPalestrantesAsync(true);

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }


       // GET: api/Palestrante/PalestranteId
        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                var result = await _respository.GetAllPalestranteAsyncById(PalestranteId, true);

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }

        // GET: api/Palestrante/PalestranteId
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _respository.Add(model);

                if(await _respository.SaveChangesAsync())
                { 
                   return Created($"/api/Palestrante/{model.Id}",model); 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }

            return BadRequest();
        }

  // GET: api/Palestrante/PalestranteId
        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {
            try
            {

                var Palestrante = await _respository.GetAllPalestranteAsyncById(PalestranteId, false);
                if(Palestrante == null) return NotFound(); 

                _respository.Update(model);

                if(await _respository.SaveChangesAsync())
                { 
                   return Created($"/api/Palestrante/{model.Id}",model); 
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }

            return BadRequest();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId){
        
        try
        {
            var palestrante = await _respository.GetAllPalestranteAsyncById(PalestranteId, false);
                if(palestrante == null) return NotFound(); 

            _respository.Delete(palestrante);    

            if(await _respository.SaveChangesAsync()){
                 return  Ok();     
            }  
        }
            catch(Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }  

            return BadRequest();
        }

           // GET: api/Palestrante/PalestranteId
        [HttpGet("getByTema/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var result = await _respository.GetAllPalestratesAsyncByName(nome, true);

                return Ok(result);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "DB failure");
            }
        }
    }
}
