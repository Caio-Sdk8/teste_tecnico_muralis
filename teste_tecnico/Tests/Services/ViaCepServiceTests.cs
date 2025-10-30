using Moq;
using Moq.Protected;
using System.Net;
using teste_tecnico.Integrations;
using Xunit;

namespace teste_tecnico.Tests.Integration;

public class ViaCepServiceTests
{
    [Fact]
    public async Task ObterEnderecoPorCepAsync_()
    {
        var json = @"{""cep"":""08810130"",""logradouro"":""Rua Nina Rodrigues"",""complemento"":"""",""bairro"":""Jundiapeba"",""localidade"":""Mogi das Cruzes"",""uf"":""SP"",""erro"":false}";
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json)
            });

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var service = new ViaCepService(httpClient);

        var result = await service.ObterEnderecoPorCepAsync("08810130");

        Assert.NotNull(result);
        Assert.Equal("08810130", result.Cep);
        Assert.Equal("Mogi das Cruzes", result.Localidade);
        Assert.False(result.Erro);
    }
}
