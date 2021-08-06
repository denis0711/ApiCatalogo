using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoriasController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos()
        {
            var categoria = _uof._categoriaRepository.GetCategoriasProdutos().ToList();

            var categoraDto = _mapper.Map<List<CategoriaDTO>>(categoria);

            return categoraDto;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            try
            {
                var categoria =_uof._categoriaRepository.Get().ToList();

                var categoraDto = _mapper.Map<List<CategoriaDTO>>(categoria);

                return categoraDto;
            
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar");
            }
            
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {

            try
            {
                var categoria = _uof._categoriaRepository.GetBydId(c => c.CategoriaId == id);
              

                if (categoria == null)
                {
                    return NotFound("Erro com o Id que voce pesquisou !");
                }
                var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

                return categoriaDto;

            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar");
            }
         
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoriaDTO categoriaDto)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);

                _uof._categoriaRepository.Add(categoria);
                _uof.Commit();

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoriaDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar uma Categoria");
            }
           
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            try
            {
                if (id != categoriaDto.CategoriaId)
                {
                    return BadRequest("Erro por Id nao existir");
                }
                var categoria = _mapper.Map<Categoria>(categoriaDto);

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
        public ActionResult<CategoriaDTO> Delete(int id)
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

                var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

                return categoriaDTO;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Deletar uma Categoria");
            }
            
        }
    }
}
