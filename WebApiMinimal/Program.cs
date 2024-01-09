// Importa os namespaces necess�rios
using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexto;
using WebApiMinimal.DTOs;
using WebApiMinimal.Models;

// Cria��o do construtor de aplica��o usando o WebApplication.CreateBuilder
var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte para gera��o de documenta��o da API
builder.Services.AddEndpointsApiExplorer();

// Ajusta o n�mero m�nimo de threads do ThreadPool
SetMinThread(10000);

// Configura��o do contexto do banco de dados usando o SQL Server
/*builder.Services.AddDbContext<Contexto>
    (options =>
    options.UseSqlServer("Data Source=DESKTOP-D39TUHI;Initial Catalog=MINIMAL_API_AULA;User ID=sa;Password=teste123;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"));
*/

builder.Services.AddDbContext<Contexto>
    (options => options.UseMySql(
        "server=localhost;Initial catalog=MINIMAL_API_AULA;uid=root;pwd=Programador01;Connection Timeout=15;",
        ServerVersion.Parse("8.0.34-mysql")));

// Configura��o do Swagger para documenta��o da API
builder.Services.AddSwaggerGen();

// Cria��o da inst�ncia da aplica��o
var app = builder.Build();

// Habilita o Swagger para gera��o de documenta��o da API
app.UseSwagger();


/// <summary>
/// M�todo para ajustar o n�mero m�nimo de threads do ThreadPool.
/// </summary>
/// <param name="total">N�mero total de threads desejado.</param>
void SetMinThread(int total)
{
    int minWorker, minIOC;
    ThreadPool.GetMinThreads(out minWorker, out minIOC);

    if (ThreadPool.SetMinThreads(total, total))
    {
        Console.WriteLine($"N�mero m�nimo de threads ajustado para {total} com sucesso.");
    }
}

// Endpoint para adicionar um novo produto
app.MapPost("AdicionaProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});

// Endpoint para atualizar um produto existente pelo seu ID
app.MapPut("AtualizaProduto/{id}", async (int id, Produto produto, Contexto contexto) =>
{
    var produtoAtualizar = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
    if (produtoAtualizar != null)
    {
        produtoAtualizar.Nome = produto.Nome; // Atributo a ser alterado
        await contexto.SaveChangesAsync();
    }
    return produtoAtualizar;
});

// Endpoint para excluir um produto pelo seu ID
app.MapDelete("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
    if (produtoExcluir != null)
    {
        contexto.Produto.Remove(produtoExcluir);
        await contexto.SaveChangesAsync();
    }
});

// Endpoint para listar todos os produtos
app.MapGet("ListarProdutos", async (Contexto contexto) =>
{

    var produtos = await contexto.Produto.ToListAsync();
    var produtosDTO = produtos.Select(p => new ProdutoDTO
    {
        Nome = p.Nome
    }).ToList();
    

    return produtosDTO;
});

// Endpoint para obter um produto pelo seu ID
app.MapGet("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
});

// Endpoint para limpar todos os produtos da tabela
app.MapDelete("LimparProdutos", async (Contexto contexto) =>
{
    // Obt�m todos os produtos da tabela
    var produtos = await contexto.Produto.ToListAsync();

    // Remove todos os produtos do contexto 
    contexto.Produto.RemoveRange(produtos);
    
    //salva as altera��es no banco de dados
    await contexto.SaveChangesAsync();

    // Retorna uma mensagem indicando que os produtos foram removidos
    return "Todos os produtos foram removidos da tabela";
});

// Habilita o Swagger UI para visualiza��o da documenta��o da API
app.UseSwaggerUI();

// Inicia a aplica��o
app.Run();
