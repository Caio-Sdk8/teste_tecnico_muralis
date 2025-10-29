namespace teste_tecnico.DTOs
{
    public class ContatoDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }

    public class CreateContatoDto
    {
        public string Tipo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }

    public class UpdateContatoDto
    {
        public int? Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }
}
