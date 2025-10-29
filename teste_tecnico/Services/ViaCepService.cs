using System.Text.Json;
using teste_tecnico.DTOs;
using teste_tecnico.Interfaces.Services;

namespace teste_tecnico.Integrations
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResponseDto?> ObterEnderecoPorCepAsync(string cep)
        {
            var cepLimpo = new string(cep.Where(char.IsDigit).ToArray());
            if (cepLimpo.Length != 8) return null;

            try
            {
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cepLimpo}/json/");
                if (!response.IsSuccessStatusCode) return null;
                var json = await response.Content.ReadAsStringAsync();
                var viaCep = JsonSerializer.Deserialize<ViaCepResponseDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return viaCep?.Erro == false ? viaCep : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
