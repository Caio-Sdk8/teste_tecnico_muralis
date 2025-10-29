namespace teste_tecnico.DTOs
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }

    public class CreateEnderecoDto
    {
        public string Cep { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }

    public class UpdateEnderecoDto
    {
        public int? Id { get; set; }
        public string Cep { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
    }
}
