using teste_tecnico.DTOs;

namespace teste_tecnico.Interfaces.Services
{
    public interface IViaCepService
    {
        Task<ViaCepResponseDto?> ObterEnderecoPorCepAsync(string cep);
    }
}
