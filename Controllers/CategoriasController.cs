using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly MeuDbContext _context;
        public CategoriasController(MeuDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar");
            }
            
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            try
            {
                var categoria = _context.Categorias.AsNoTracking()
                .FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound("Erro com o Id que voce pesquisou !");
                }
                return categoria;

            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar");
            }
         
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar uma Categoria");
            }
           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Erro por Id nao existir");
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Categoria atualizada com Sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Atualizar uma Categoria");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
                //var categoria = _context.Categorias.Find(id);

                if (categoria == null)
                {
                    return NotFound("Erro ao deletar a Categoria");
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Deletar uma Categoria");
            }
            
        }
    }
}
