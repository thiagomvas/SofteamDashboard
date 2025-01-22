using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Application;

public class SofteamDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<HabilidadeFuncionario> HabilidadeFuncionarios { get; set; }
    public DbSet<Projeto> Projetos { get; set; }

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

            // Many-To-One com Projeto
            entity.HasOne(e => e.Projeto)
                .WithMany(p => p.Funcionarios)
                .HasForeignKey(e => e.ProjetoId)
                .IsRequired(false) 
                .OnDelete(DeleteBehavior.Restrict); 

            entity.Property(e => e.ProjetoId)
                .HasDefaultValue(1); 
        });

        modelBuilder.Entity<HabilidadeFuncionario>(entity =>
        {
            entity.HasIndex(e => e.Id);
            entity.HasOne(e => e.Funcionario)
                .WithMany(f => f.Habilidades)
                .HasForeignKey(e => e.FuncionarioId);
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasIndex(e => e.Id);
            entity.Property(e => e.Inicio).HasColumnType("datetime");
            entity.Property(e => e.Fim).HasColumnType("datetime");
            entity.HasMany(e => e.Funcionarios)
                .WithOne(f => f.Projeto)
                .HasForeignKey(f => f.ProjetoId);

            entity.HasOne(p => p.Responsavel)
                .WithMany() 
                .HasForeignKey(p => p.ResponsavelId);
        });

        base.OnModelCreating(modelBuilder);
    }

}