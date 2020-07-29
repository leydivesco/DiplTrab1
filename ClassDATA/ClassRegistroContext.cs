using ClassRegistro.Model;
using ClassRegistro.Model.Cafeteria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassDATA
{
    //CodeFirst
   public class ClassRegistroContext : DbContext 
    {
        public ClassRegistroContext(DbContextOptions<ClassRegistroContext>options):
            base(options)
        {

        }
        public DbSet<ClassMateria> Materias { get; set; }
        public DbSet<ClassEstudiante>Estudiante { get; set; }
        public DbSet<ClassCategoria> Categorias { get; set; }
        public DbSet<ClassProducto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassCategoria>().HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<ClassCategoria>()
                .Property(c => c.Nombre)
                .HasMaxLength(50);

            modelBuilder.Entity<ClassCategoria>()
                .Property(c => c.Nombre)
                .IsRequired(true);
        }
    }
}
