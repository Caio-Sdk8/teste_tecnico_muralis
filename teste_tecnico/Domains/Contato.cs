namespace teste_tecnico.Domains;

public class Contato
{
    public int Id { get; private set; }
    public string Tipo { get; private set; } = string.Empty;
    public string Texto { get; private set; } = string.Empty;

    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;

    internal Contato(string tipo, string texto, int clienteId)
    {
        Tipo = tipo;
        Texto = texto;
        ClienteId = clienteId;
    }

    internal Contato(Guid id, string tipo, string texto, int clienteId)
    {
        Tipo = tipo;
        Texto = texto;
        ClienteId = clienteId;
    }

    public void Atualizar(string tipo, string texto)
    {
        Tipo = tipo;
        Texto = texto;
    }

    private Contato() { }
}
