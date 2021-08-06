using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
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
        private readonly IUnitOfWork _uof;
        public CategoriasController(IUnitOfWork contexto)
        {
            _uof = contexto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return _uof._categoriaRepository.GetCategoriasProdutos().ToList();
        }
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _uof._categoriaRepository.Get().ToList();
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
                var categoria = _uof._categoriaRepository.GetBydId(c => c.CategoriaId == id);

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
                _uof._categoriaRepository.Add(categoria);
                _uof.Commit();

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

                _uof._categoriaRepository.Update(categoria);
                _uof.Commit();
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
                var categoria = _uof._categoriaRepository.GetBydId(c => c.CategoriaId == id);
                    //var categoria = _uof.Categorias.Find(id);

                if (categoria == null)
                {
                    return NotFound("Erro ao deletar a Categoria");
                }
                _uof._categoriaRepository.Delete(categoria);
                _uof.Commit();
                return categoria;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Deletar uma Categoria");
            }
            
        }
    }
}
