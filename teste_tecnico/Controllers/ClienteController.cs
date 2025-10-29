using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using teste_tecnico.DTOs;
using teste_tecnico.Interfaces.Services;

namespace teste_tecnico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<ClienteDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ClienteDto>>> Listar(
            [FromQuery] string? busca = null,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanho = 10)
        {
            // Validação simples
            if (pagina < 1) pagina = 1;
            if (tamanho < 1) tamanho = 10;
            if (tamanho > 50) tamanho = 50;

            var (clientes, total) = await _clienteService.ListarAsync(busca, pagina, tamanho);
            var resultado = new PaginatedResult<ClienteDto>(clientes, total, pagina, tamanho);
            return Ok(resultado);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> ObterPorId(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            return Ok(cliente);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClienteDto>> Criar([FromBody] CreateClienteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("O nome do cliente é obrigatório.");

            try
            {
                var cliente = await _clienteService.CriarAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Atualizar([FromBody] UpdateClienteDto dto)
        {
            if (dto.Id == 0)
                return BadRequest("ID do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("O nome do cliente é obrigatório.");

            try
            {
                var cliente = await _clienteService.AtualizarAsync(dto);
                return Ok(cliente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _clienteService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente não encontrado.");
            }
        }
    }
}
