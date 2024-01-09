namespace WebApiMinimal.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) para a entidade Produto.
    /// </summary>
    public class ProdutoDTO
    {
        /// <summary>
        /// Coleta ou define o identificador único do DTO de produto.
        /// </summary>
       // public int Id { get; set; } // OBS: Deixei ID como comentário para ter diferenças entre o DTO e o model de produto. Numa aplicação mais complexa, faz sentido ter ID sim  
       

        /// <summary>
        /// Coleta ou define o nome do DTO de produto.
        /// </summary>
        public string Nome { get; set; }
    }
}
