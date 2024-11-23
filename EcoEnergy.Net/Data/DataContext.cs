using EcoEnergy.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }

        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<Simulacao> Simulacao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Empresa> Empresa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("SEQ_T_GS24_ORCAMENTO").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Orcamento>()
                .Property(o => o.IdOrcamento)
                .HasDefaultValueSql("SEQ_T_GS24_ORCAMENTO.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_GS24_SIMULACAO").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Simulacao>()
                .Property(s => s.IdSimulacao)
                .HasDefaultValueSql("SEQ_T_GS24_SIMULACAO.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_GS24_ENDERECO").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Endereco>()
                .Property(e => e.IdEndereco)
                .HasDefaultValueSql("SEQ_T_GS24_ENDERECO.NEXTVAL");

            modelBuilder.HasSequence("SEQ_T_GS24_USUARIO").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Usuario>()
                .Property(u => u.IdUsuario)
                .HasDefaultValueSql("SEQ_T_GS24_USUARIO.NEXTVAL");


            modelBuilder.HasSequence("SEQ_T_GS24_EMPRESA").StartsAt(1).IncrementsBy(1);
            modelBuilder.Entity<Empresa>()
                .Property(e => e.IdEmpresa)
                .HasDefaultValueSql("SEQ_T_GS24_EMPRESA.NEXTVAL");

            base.OnModelCreating(modelBuilder);
        }
    }
}
