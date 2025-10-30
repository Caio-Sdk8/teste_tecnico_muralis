using teste_tecnico.Domains;
using Xunit;

namespace teste_tecnico.Tests.Domain
{
    public class ClienteTests
    {
        [Fact]
        public void CriarCliente()
        {
            var cliente = new Cliente("Caio");

            Assert.Equal("Caio", cliente.Nome);
            Assert.NotEqual(default, cliente.DataCadastro);
            Assert.True(cliente.DataCadastro <= DateTime.UtcNow);
            Assert.Empty(cliente.Contatos);
            Assert.Null(cliente.Endereco);
        }

        [Fact]
        public void AdicionarContato()
        {
            var cliente = new Cliente("Caio");

            cliente.AdicionarContato("telefone", "(11) 99999-9999");

            Assert.Single(cliente.Contatos);
            var contato = cliente.Contatos.First();
            Assert.Equal("telefone", contato.Tipo);
            Assert.Equal("(11) 99999-9999", contato.Texto);
        }

        [Fact]
        public void DefinirEndereco()
        {
            var cliente = new Cliente("Caio");

            cliente.DefinirEndereco("08810130", "Rua Nina Rodrigues", "Mogi das Cruzes", "353", "Casa");

            Assert.NotNull(cliente.Endereco);
            Assert.Equal("08810130", cliente.Endereco.Cep);
            Assert.Equal("Mogi das Cruzes", cliente.Endereco.Cidade);
            Assert.Equal("353", cliente.Endereco.Numero);
        }
    }
}
