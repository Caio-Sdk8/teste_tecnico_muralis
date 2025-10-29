namespace teste_tecnico.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public EnderecoDto? Endereco { get; set; }
        public List<ContatoDto> Contatos { get; set; } = new();
    }
    public class CreateClienteDto
    {
        public string Nome { get; set; } = string.Empty;
        public CreateEnderecoDto? Endereco { get; set; }
        public List<CreateContatoDto> Contatos { get; set; } = new();
    }

    public class UpdateClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public UpdateEnderecoDto? Endereco { get; set; }
        public List<UpdateContatoDto> Contatos { get; set; } = new();
    }
}
