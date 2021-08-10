using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public interface IUnitOfWork
    {
        IProdutoRepository _produtoRepository { get; }
        ICategoriaRepository _categoriaRepository { get; }

        Task Commit();
        
    }
}
