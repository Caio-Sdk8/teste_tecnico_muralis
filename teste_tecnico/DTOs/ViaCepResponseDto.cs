namespace teste_tecnico.DTOs
{
    public class ViaCepResponseDto
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty; // Cidade
        public string Uf { get; set; } = string.Empty;
        public bool Erro { get; set; }
    }
}
