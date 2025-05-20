using Microsoft.EntityFrameworkCore;
using MinimalApi.Cursos.Data.Entities;

namespace MinimalApi.Cursos.Data
{
    public class CursosDbContext(DbContextOptions op) : DbContext(op)
    {
        public DbSet<Curso> Cursos { get; set; }
    }
}
