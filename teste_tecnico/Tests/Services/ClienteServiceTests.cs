using AutoMapper;
using Bogus;
using Moq;
using teste_tecnico.Domains;
using teste_tecnico.DTOs;
using teste_tecnico.Interfaces;
using teste_tecnico.Interfaces.Services;
using teste_tecnico.Profiles;
using teste_tecnico.Services;
using Xunit;

namespace teste_tecnico.Tests.Services;

public class ClienteServiceTests
{
    private readonly Mock<IClienteRepository> _mockRepo;
    private readonly Mock<IViaCepService> _mockViaCep;
    private readonly IMapper _mapper;
    private readonly ClienteService _service;
    private readonly Faker _faker;
    private static void SetPrivatePropertyValue<T>(object obj, string propertyName, T value)
    {
        var property = obj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        property?.SetValue(obj, value);
    }

    public ClienteServiceTests()
    {
        _mockRepo = new Mock<IClienteRepository>();
        _mockViaCep = new Mock<IViaCepService>();
        _faker = new Faker();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();

        _service = new ClienteService(_mockRepo.Object, _mockViaCep.Object, _mapper);
    }

    [Fact]
    public async Task CriarAsync()
    {
        // Arrange
        var nome = _faker.Name.FullName();
        var cep = "08810130";
        var numero = _faker.Address.BuildingNumber();
        var complemento = _faker.Lorem.Word();
        var telefone = _faker.Phone.PhoneNumber("(##) #####-####");
        var email = _faker.Internet.Email();

        var dto = new CreateClienteDto
        {
            Nome = nome,
            Endereco = new CreateEnderecoDto
            {
                Cep = cep,
                Numero = numero,
                Complemento = complemento
            },
            Contatos = new List<CreateContatoDto>
            {
                new() { Tipo = "telefone", Texto = telefone },
                new() { Tipo = "email", Texto = email }
            }
        };

        var viaCepResponse = new ViaCepResponseDto
        {
            Cep = cep,
            Logradouro = "Rua Nina Rodrigues",
            Localidade = "Mogi das Cruzes",
            Erro = false
        };

        _mockViaCep
            .Setup(v => v.ObterEnderecoPorCepAsync(cep))
            .ReturnsAsync(viaCepResponse);

        Cliente? clienteSalvo = null;
        _mockRepo
            .Setup(r => r.AdicionarAsync(It.IsAny<Cliente>()))
            .Callback<Cliente>(c => clienteSalvo = c)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CriarAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(nome, result.Nome);
        Assert.NotNull(result.Endereco);
        Assert.Equal("Mogi das Cruzes", result.Endereco.Cidade);
        Assert.Equal(2, result.Contatos.Count);
        Assert.Contains(result.Contatos, c => c.Texto == telefone);
        Assert.Contains(result.Contatos, c => c.Texto == email);
    }
}
