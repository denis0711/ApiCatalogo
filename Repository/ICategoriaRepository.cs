using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategoriasPaginas(CategoriasParameters categoriasParameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
