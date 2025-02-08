using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;

namespace SofteamDashboard.Server.Services
{
    public class SeedService
    {
        private readonly SofteamDbContext _context;
        
        public SeedService(SofteamDbContext context)
        {
            _context = context;
        }
        
        public async Task SeedAsync()
        {
            await SeedPermissoesAsync();
            await SeedCargosAsync();
            await _context.SaveChangesAsync();
            
            await SeedCargoPermissionsAsync();
            await _context.SaveChangesAsync();
        }

        private async Task SeedCargosAsync()
        {
            List<Cargo> Cargos = new List<Cargo>
            {
                new Cargo() { Nome = Constants.DIRETOR, Descricao = "Cargo administrativo de algum setor em especifico, inclui presidentes." },
                new Cargo() { Nome = Constants.DEV, Descricao = "Membro da equipe de projetos" },
                new Cargo() { Nome = Constants.RH, Descricao = "Membro da equipe de RH" },
                new Cargo() { Nome = Constants.FINANCEIRO, Descricao = "Membro da equipe financeira" },
                new Cargo() { Nome = Constants.MARKETING, Descricao = "Membro da equipe de marketing" }
            };
            
            // Add any non existent cargos
            foreach (var cargo in Cargos)
            {
                if (!_context.Cargos.Any(c => c.Nome == cargo.Nome))
                    _context.Cargos.Add(cargo);
            }
        }

        private async Task SeedCargoPermissionsAsync()
        {
            List<PermissaoCargo> PermissaoCargos = new List<PermissaoCargo>();
            
            // Diretor
            var diretor = await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == Constants.DIRETOR) ?? throw new Exception("Diretor not found");
            var permissoes = await GetPermissoes(Constants.ADMIN, Constants.MANAGE_FUNCIONARIOS, Constants.MANAGE_CARGOS, Constants.MANAGE_PROJETOS, Constants.VIEW_METRICAS, Constants.VIEW_FUNCIONARIOS, Constants.VIEW_CARGOS, Constants.VIEW_PROJETOS);
            PermissaoCargos.AddRange(permissoes.Select(permissao => new PermissaoCargo() { CargoId = diretor.Id, PermissaoId = permissao.Id }));
            
            // Dev
            var dev = await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == Constants.DEV);
            permissoes = await GetPermissoes(Constants.VIEW_METRICAS, Constants.VIEW_FUNCIONARIOS, Constants.VIEW_CARGOS, Constants.VIEW_PROJETOS);
            PermissaoCargos.AddRange(permissoes.Select(permissao => new PermissaoCargo() { CargoId = dev.Id, PermissaoId = permissao.Id }));
            
            // RH
            var rh = await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == Constants.RH);
            permissoes = await GetPermissoes(Constants.VIEW_FUNCIONARIOS, Constants.VIEW_CARGOS, Constants.VIEW_PROJETOS);
            PermissaoCargos.AddRange(permissoes.Select(permissao => new PermissaoCargo() { CargoId = rh.Id, PermissaoId = permissao.Id }));
            
            // Financeiro
            var financeiro = await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == Constants.FINANCEIRO);
            permissoes = await GetPermissoes(Constants.VIEW_METRICAS, Constants.VIEW_FUNCIONARIOS, Constants.VIEW_CARGOS, Constants.VIEW_PROJETOS);
            PermissaoCargos.AddRange(permissoes.Select(permissao => new PermissaoCargo() { CargoId = financeiro.Id, PermissaoId = permissao.Id }));
            
            // Marketing
            var marketing = await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == Constants.MARKETING);
            permissoes = await GetPermissoes(Constants.VIEW_METRICAS, Constants.VIEW_FUNCIONARIOS, Constants.VIEW_CARGOS, Constants.VIEW_PROJETOS);
            PermissaoCargos.AddRange(permissoes.Select(permissao => new PermissaoCargo() { CargoId = marketing.Id, PermissaoId = permissao.Id }));

            // Add any non existent permissao cargos
            foreach (var permissaoCargo in PermissaoCargos)
            {
                if (!_context.PermissaoCargos.Any(pc => pc.CargoId == permissaoCargo.CargoId && pc.PermissaoId == permissaoCargo.PermissaoId))
                    _context.PermissaoCargos.Add(permissaoCargo);
            }
        }

        private async Task SeedPermissoesAsync()
        {
            List<Permissao> Permissoes = new List<Permissao>
            {
                new Permissao() { Nome = Constants.ADMIN, Descricao = "Garante acesso a todos os recursos do sistema." },
                new Permissao() { Nome = Constants.MANAGE_FUNCIONARIOS, Descricao = "Permite gerenciar funcionários." },
                new Permissao() { Nome = Constants.MANAGE_CARGOS, Descricao = "Permite gerenciar cargos." },
                new Permissao() { Nome = Constants.MANAGE_PROJETOS, Descricao = "Permite gerenciar projetos." },
                new Permissao() { Nome = Constants.VIEW_METRICAS, Descricao = "Permite visualizar métricas." },
                new Permissao() { Nome = Constants.VIEW_FUNCIONARIOS, Descricao = "Permite visualizar funcionários." },
                new Permissao() { Nome = Constants.VIEW_CARGOS, Descricao = "Permite visualizar cargos." },
                new Permissao() { Nome = Constants.VIEW_PROJETOS, Descricao = "Permite visualizar projetos." }
            };
            
            // Add any non existent permissions
            foreach (var permissao in Permissoes)
            {
                if (!_context.Permissoes.Any(p => p.Nome == permissao.Nome))
                    _context.Permissoes.Add(permissao);
            }
        }
        
        private async Task<IEnumerable<Permissao>> GetPermissoes(params string[] permissoes)
        {
            return await _context.Permissoes.AsNoTracking().Where(p => permissoes.Contains(p.Nome)).ToListAsync();
        }
    }
}
