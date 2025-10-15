using Microsoft.EntityFrameworkCore;
using Fiap.Web.Api.Desperdicio.Models;

namespace Fiap.Web.Api.Desperdicio.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<Impacto> Impactos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. CONFIGURAÇÃO DO MODELO ALIMENTO
            modelBuilder.Entity<Alimento>(entity =>
            {
                entity.ToTable("alimentos");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.QuantidadeKg)
                    .IsRequired();

                entity.Property(e => e.DataValidade)
                    .IsRequired();

                entity.Property(e => e.Doado)
                    .IsRequired();
            });

            modelBuilder.Entity<Impacto>(entity =>
            {
                entity.ToTable("impactos");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TotalKgReaproveitados)
                    .IsRequired();

                entity.Property(e => e.TotalRefeicoesGeradas)
                    .IsRequired();

                entity.Property(e => e.Co2EconomizadoKg)
                    .IsRequired();

                entity.Property(e => e.DataReferencia)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
