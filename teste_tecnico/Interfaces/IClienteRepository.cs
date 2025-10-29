using teste_tecnico.Domains;

namespace teste_tecnico.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<(List<Cliente> Clientes, int Total)> ObterComFiltroEPaginacaoAsync(
            string? termo = null, int pagina = 1, int tamanho = 10);
        Task AdicionarAsync(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(Cliente cliente);
        Task<int> SaveChangesAsync();
    }
}
