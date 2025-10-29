namespace teste_tecnico.Domains;

public class Endereco
{
    public int Id { get; private set; }
    public string Cep { get; private set; } = string.Empty;
    public string Logradouro { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string Numero { get; private set; } = string.Empty;
    public string Complemento { get; private set; } = string.Empty;

    public int ClienteId { get; private set; }

    public Cliente Cliente { get; private set; } = null!;

    internal Endereco(string cep, string logradouro, string cidade, string numero, string complemento, int clienteId)
    {
        Cep = cep;
        Logradouro = logradouro;
        Cidade = cidade;
        Numero = numero;
        Complemento = complemento;
        ClienteId = clienteId;
    }

    public void Atualizar(string cep, string logradouro, string cidade, string numero, string complemento)
    {
        Cep = cep;
        Logradouro = logradouro;
        Cidade = cidade;
        Numero = numero;
        Complemento = complemento;
    }
    private Endereco() { }
}
