using ApiCatalogo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Context
{
    public class MeuDbContext : IdentityDbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base (options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        
    }
}
