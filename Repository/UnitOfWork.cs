using ApiCatalogo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProdutoRepository produtoRepository;
        private CategoriaRepository categoriaRepository;
        public MeuDbContext _dbContext;
        public IProdutoRepository _produtoRepository
        {
            get
            {
                return produtoRepository = produtoRepository ?? new ProdutoRepository(_dbContext);
            }
        }

        public ICategoriaRepository _categoriaRepository
        {
            get
            {
                return categoriaRepository = categoriaRepository ?? new CategoriaRepository(_dbContext);
            }
        }

        public UnitOfWork(MeuDbContext context)
        {
            _dbContext = context;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
