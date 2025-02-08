using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Core;

public class SofteamDbContext : DbContext
{
    public SofteamDbContext(DbContextOptions<SofteamDbContext> options) : base(options)
    {
    }

    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Permissao> Permissoes { get; set; }
    public DbSet<PermissaoCargo> PermissaoCargos { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<MembroProjeto> MembroProjetos { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Credenciais> Credenciais { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasOne(f => f.Cargo)
                .WithMany()
                .HasForeignKey(f => f.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(f => f.Alocacoes)
                .WithOne(mp => mp.Funcionario)
                .HasForeignKey(a => a.FuncionarioId);

            entity.HasKey(f => f.Id);
        });
        
        modelBuilder.Entity<MembroProjeto>(entity =>
        {
            entity.HasOne(mp => mp.Funcionario)
                .WithMany(f => f.Alocacoes)
                .HasForeignKey(mp => mp.FuncionarioId);

            entity.HasOne(mp => mp.Projeto)
                .WithMany(p => p.Membros)
                .HasForeignKey(mp => mp.ProjetoId);

            entity.HasKey(mp => mp.Id);
        });
        
        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasMany(p => p.Membros)
                .WithOne(mp => mp.Projeto)
                .HasForeignKey(mp => mp.ProjetoId);

            entity.HasKey(p => p.Id);
        });
        
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.HasMany(c => c.Permissoes)
                .WithOne(pc => pc.Cargo)
                .HasForeignKey(pc => pc.CargoId);
        });
        
        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(p => p.Id);
        });
        
        modelBuilder.Entity<PermissaoCargo>(entity =>
        {
            entity.HasKey(pc => pc.Id);
            
            entity.HasOne(pc => pc.Cargo)
                .WithMany(c => c.Permissoes)
                .HasForeignKey(pc => pc.CargoId);

            entity.HasOne(pc => pc.Permissao)
                .WithMany()
                .HasForeignKey(pc => pc.PermissaoId);
        });
        
        modelBuilder.Entity<Credenciais>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.HasOne(c => c.Funcionario)
                .WithOne(f => f.Credenciais)
                .HasForeignKey<Credenciais>(c => c.FuncionarioId);
        });
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity is BaseEntity && entry.State == EntityState.Added))
        {
            ((BaseEntity)entry.Entity).CreatedAt = DateTime.Now;
        }
        
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity is BaseEntity && entry.State == EntityState.Modified))
        {
            ((BaseEntity)entry.Entity).UpdatedAt = DateTime.Now;
        }
        
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
}