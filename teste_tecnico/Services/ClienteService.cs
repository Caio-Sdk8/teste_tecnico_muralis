using AutoMapper;
using teste_tecnico.Domains;
using teste_tecnico.DTOs;
using teste_tecnico.Interfaces;
using teste_tecnico.Interfaces.Services;
using teste_tecnico.Repositories;
using teste_tecnico.Services;

namespace teste_tecnico.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IViaCepService _viaCepService;
    private readonly IMapper _mapper;

    public ClienteService(
        IClienteRepository repository,
        IViaCepService viaCepService,
        IMapper mapper)
    {
        _repository = repository;
        _viaCepService = viaCepService;
        _mapper = mapper;
    }

    public async Task<ClienteDto> CriarAsync(CreateClienteDto dto)
    {
        var cliente = new Cliente(dto.Nome);

        // Endereço (opcional)
        if (dto.Endereco != null)
        {
            var viaCep = await _viaCepService.ObterEnderecoPorCepAsync(dto.Endereco.Cep);
            if (viaCep == null)
                throw new ArgumentException("CEP inválido ou não encontrado.");

            cliente.DefinirEndereco(
                cep: dto.Endereco.Cep,
                logradouro: viaCep.Logradouro,
                cidade: viaCep.Localidade,
                numero: dto.Endereco.Numero,
                complemento: dto.Endereco.Complemento
            );
        }

        foreach (var contato in dto.Contatos)
        {
            cliente.AdicionarContato(contato.Tipo, contato.Texto);
        }

        await _repository.AdicionarAsync(cliente);
        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> AtualizarAsync(UpdateClienteDto dto)
    {
        var clienteExistente = await _repository.ObterPorIdAsync(dto.Id);
        if (clienteExistente == null)
            throw new KeyNotFoundException("Cliente não encontrado.");

        clienteExistente.AtualizarNome(dto.Nome);

        if (dto.Endereco != null)
        {
            var viaCep = await _viaCepService.ObterEnderecoPorCepAsync(dto.Endereco.Cep);
            if (viaCep == null)
                throw new ArgumentException("CEP inválido ou não encontrado.");

            if (clienteExistente.Endereco != null)
            {
                clienteExistente.Endereco.Atualizar(
                    dto.Endereco.Cep,
                    viaCep.Logradouro,
                    viaCep.Localidade,
                    dto.Endereco.Numero,
                    dto.Endereco.Complemento
                );
            }
            else
            {
                clienteExistente.DefinirEndereco(
                    dto.Endereco.Cep,
                    viaCep.Logradouro,
                    viaCep.Localidade,
                    dto.Endereco.Numero,
                    dto.Endereco.Complemento
                );
            }
        }

        var contatosParaAtualizar = dto.Contatos.Select(c => (c.Id, c.Tipo, c.Texto)).ToList();
        clienteExistente.AtualizarContatos(contatosParaAtualizar);

        _repository.Atualizar(clienteExistente);
        await _repository.SaveChangesAsync();

        var clienteAtualizado = await _repository.ObterPorIdAsync(clienteExistente.Id);
        return _mapper.Map<ClienteDto>(clienteAtualizado!);
    }

    public async Task<ClienteDto?> ObterPorIdAsync(int id)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<(List<ClienteDto> Clientes, int Total)> ListarAsync(
        string? termo = null, int pagina = 1, int tamanho = 10)
    {
        var (clientes, total) = await _repository.ObterComFiltroEPaginacaoAsync(termo, pagina, tamanho);
        var dtos = _mapper.Map<List<ClienteDto>>(clientes);
        return (dtos, total);
    }

    public async Task RemoverAsync(int id)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        if (cliente == null)
            throw new KeyNotFoundException("Cliente não encontrado.");

        _repository.Remover(cliente);
        await _repository.SaveChangesAsync();
    }
}
