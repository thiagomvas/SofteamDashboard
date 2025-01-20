using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Application;

public class SofteamDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<HabilidadeFuncionario> HabilidadeFuncionarios { get; set; }

    public SofteamDbContext(DbContextOptions<SofteamDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();
            
            // One-To-Many com HabilidadeFuncionario
            entity.HasMany(e => e.Habilidades)
                .WithOne(h => h.Funcionario)
                .HasForeignKey(h => h.FuncionarioId)
                .OnDelete(DeleteBehavior.Cascade);
            
        });

        modelBuilder.Entity<HabilidadeFuncionario>(entity =>
        {
            entity.HasIndex(e => e.Id);
            entity.HasOne(e => e.Funcionario)
                .WithMany(f => f.Habilidades)
                .HasForeignKey(e => e.FuncionarioId);
        });
        base.OnModelCreating(modelBuilder);
    }
}