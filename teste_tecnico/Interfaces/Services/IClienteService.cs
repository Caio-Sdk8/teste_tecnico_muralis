using teste_tecnico.DTOs;

namespace teste_tecnico.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteDto> CriarAsync(CreateClienteDto dto);
        Task<ClienteDto> AtualizarAsync(UpdateClienteDto dto);
        Task<ClienteDto?> ObterPorIdAsync(int id);
        Task<(List<ClienteDto> Clientes, int Total)> ListarAsync(
            string? termo = null, int pagina = 1, int tamanho = 10);
        Task RemoverAsync(int id);
    }
}
