namespace WebApiMinimal.Models
{
    public class Produto
    {
        /// <summary>
        /// Obtém ou define o identificador único do produto. Este é um atributo autoincrementado.
        /// </summary>
        public int Id { get; set; } //Atributo Id

        /// <summary>
        /// Obtém ou define o nome do produto. Pode ser nulo, indicando que o nome não é obrigatório.
        /// </summary>
        public string? Nome { get; set; } //Atributo nome
    }
}
