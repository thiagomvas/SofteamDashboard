
using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;

var options = new DbContextOptionsBuilder<SofteamDbContext>()
    .UseInMemoryDatabase("testdb")
    .Options;
    
using var context = new SofteamDbContext(options);

var cargo = new Cargo { Nome = "Desenvolvedor", Descricao = "Responsavel por desenvolver software" };
var funcionario = new Funcionario { Nome = "João", Cargo = cargo };
var projeto = new Projeto { Nome = "Projeto 1" };

context.Cargos.Add(cargo);
context.Funcionarios.Add(funcionario);
context.Projetos.Add(projeto);

await context.SaveChangesAsync();

var membroProjeto = new MembroProjeto { Funcionario = funcionario, Projeto = projeto };
context.MembroProjetos.Add(membroProjeto);

await context.SaveChangesAsync();

var projetos = await context.Projetos
    .Include(p => p.Membros)
    .ThenInclude(mp => mp.Funcionario)
    .ToListAsync();

foreach (var p in projetos)
{
    Console.WriteLine($"Projeto: {p.Nome}");
    foreach (var mp in p.Membros)
    {
        Console.WriteLine($"Membro: {mp.Funcionario.Nome}");
    }
}
