using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Models;

namespace WebApiMinimal.Contexto
{

    /// <summary>
    /// A classe <c>Contexto</c> representa o contexto de banco de dados para a aplicação.
    /// Ela gerencia as interações com o banco de dados e mapeia as entidades para tabelas. Ou seja, ela trabalha em conjunto com a ORM para transformar as classes C# \
    /// </summary>
    public class Contexto : DbContext
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <c>Contexto</c>.
        /// </summary>
        /// <param name="options">Opções de configuração para o contexto de banco de dados.</param>
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) =>  Database.EnsureCreated();

        /// <summary>
        /// Métodos get e set para entidade Produto no banco de dados.
        /// </summary>
        public DbSet<Produto> Produto { get; set; }

    }
}
