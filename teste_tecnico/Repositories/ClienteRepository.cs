using Microsoft.EntityFrameworkCore;
using teste_tecnico.Data;
using teste_tecnico.Domains;
using teste_tecnico.Interfaces;
using teste_tecnico.Repositories;

namespace teste_tecnico.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> ObterPorIdAsync(int id)
    {
        return await _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<(List<Cliente> Clientes, int Total)> ObterComFiltroEPaginacaoAsync(
        string? termo = null, int pagina = 1, int tamanho = 10)
    {
        var query = _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(termo))
        {
            var t = termo.Trim().ToLower();
            query = query.Where(c =>
                c.Nome.ToLower().Contains(t) ||
                (c.Endereco != null && (
                    c.Endereco.Cep.Contains(t) ||
                    c.Endereco.Cidade.ToLower().Contains(t) ||
                    c.Endereco.Logradouro.ToLower().Contains(t)
                )) ||
                c.Contatos.Any(ct =>
                    ct.Tipo.ToLower().Contains(t) ||
                    ct.Texto.ToLower().Contains(t)
                )
            );
        }

        var total = await query.CountAsync();
        var clientes = await query
            .Skip((pagina - 1) * tamanho)
            .Take(tamanho)
            .ToListAsync();

        return (clientes, total);
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public void Atualizar(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
    }

    public void Remover(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
