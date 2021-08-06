using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork context)
        {
            _uof = context;
        }


        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return _uof._produtoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]

        public ActionResult<IEnumerable<Produto>> Get()
        {
            //AsNoTracking desabilita o gerenciamento do estado das entidades
            //so deve ser usado em consultas sem alteração

            return  _uof._produtoRepository.Get().ToList();
        }


        //Teve que colocar isso para nao da sobrecarga com os metodos com os mesmo nome e rota
        [HttpGet("{id}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uof._produtoRepository.GetBydId(p=>p.Id == id);

            if(produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            //a validação do ModelState é feito automaticamente
            //quando aplicamos o atributo [ApiController]

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uof._produtoRepository.Add(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id}, produto);
        }


        [HttpPut]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != produto.Id)
            {
                return BadRequest();
            }

            _uof._produtoRepository.Update(produto);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _uof._produtoRepository.GetBydId(p => p.Id == id);

            if(produto == null)
            {
                return NotFound();
            }

            _uof._produtoRepository.Delete(produto);
            _uof.Commit();

            return produto;

        }
    }
}
