namespace teste_tecnico.Domains;

public class Cliente
{
    private readonly List<Contato> _contatos = new();

    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public DateTime DataCadastro { get; private set; }

    public Endereco? Endereco { get; private set; }

    public IReadOnlyCollection<Contato> Contatos => _contatos.AsReadOnly();

    public Cliente(string nome)
    {
        Nome = nome;
        DataCadastro = DateTime.UtcNow;
    }

    public void DefinirEndereco(string cep, string logradouro, string cidade, string numero, string complemento)
    {
        Endereco = new Endereco(cep, logradouro, cidade, numero, complemento, Id);
    }

    public void AdicionarContato(string tipo, string texto)
    {
        _contatos.Add(new Contato(tipo, texto, Id));
    }

    public void AtualizarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(novoNome));
        Nome = novoNome;
    }

    public void AtualizarContatos(List<(int? Id, string Tipo, string Texto)> contatosDto)
    {
        var idsMantidos = new HashSet<int>();

        foreach (var dto in contatosDto)
        {
            if (dto.Id.HasValue && dto.Id > 0)
            {
                var existente = _contatos.FirstOrDefault(c => c.Id == dto.Id);
                if (existente != null)
                {
                    existente.Atualizar(dto.Tipo, dto.Texto);
                    idsMantidos.Add(dto.Id.Value);
                }
            }
            else
            {
                _contatos.Add(new Contato(dto.Tipo, dto.Texto, Id));
            }
        }

        _contatos.RemoveAll(c => c.Id > 0 && !idsMantidos.Contains(c.Id));
    }
    private Cliente() { }
}
