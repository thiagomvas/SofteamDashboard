using Microsoft.EntityFrameworkCore;
using SofteamDashboard.Application;
using SofteamDashboard.Core;
using SofteamDashboard.Core.Entities;
using SofteamDashboard.Core.ValueTypes;

namespace SofteamDashboard.Tests;

public class DbContextTests
{
    private SofteamDbContext context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SofteamDbContext>()
            .UseInMemoryDatabase("softeam-db-tests")
            .Options;

        context = new SofteamDbContext(options);
    }
    
    [TearDown]
    public void TearDown()
    {
        context.Dispose();
    }

    [Test]
    public void DeleteFuncionario_DeveRemover_TodasHabilidades()
    {
        var funcionario = new Funcionario()
        {
            Nome = "John Doe",
            Area = Area.Projetos,
            Cargo = Cargo.Membro,
            Habilidades = new List<HabilidadeFuncionario>()
            {
                new HabilidadeFuncionario()
                {
                    NomeHabilidade = "React",
                    Nivel = Nivel.Intermediario,
                    Verificado = false
                },
                new()
                {
                    NomeHabilidade = "Angular",
                    Nivel = Nivel.Intermediario,
                    Verificado = true
                }
            }
        };
        
        context.Funcionarios.Add(funcionario);
        context.SaveChanges();
        
        Assert.That(context.HabilidadeFuncionarios.Count(), Is.EqualTo(2));
        Assert.That(context.Funcionarios.Count(), Is.EqualTo(1));
        
        context.Funcionarios.Remove(funcionario);
        context.SaveChanges();
        
        Assert.That(context.HabilidadeFuncionarios.Count(), Is.EqualTo(0));
    }

    [Test]
    public void DeleteHabiliade_NaoDeveRemover_Funcionario()
    {
        
        var funcionario = new Funcionario()
        {
            Nome = "John Doe",
            Area = Area.Projetos,
            Cargo = Cargo.Membro,
            Habilidades = new List<HabilidadeFuncionario>()
            {
                new HabilidadeFuncionario()
                {
                    NomeHabilidade = "React",
                    Nivel = Nivel.Intermediario,
                    Verificado = false
                },
                new()
                {
                    NomeHabilidade = "Angular",
                    Nivel = Nivel.Intermediario,
                    Verificado = true
                }
            }
        };
        
        context.Funcionarios.Add(funcionario);
        context.SaveChanges();
        
        Assert.That(context.HabilidadeFuncionarios.Count(), Is.EqualTo(2));
        Assert.That(context.Funcionarios.Count(), Is.EqualTo(1));
        
        var habilidade = funcionario.Habilidades.First();
        context.HabilidadeFuncionarios.Remove(habilidade);
        context.SaveChanges();
        
        Assert.That(context.HabilidadeFuncionarios.Count(), Is.EqualTo(1));
        Assert.That(context.Funcionarios.Count(), Is.EqualTo(1));
    }
}