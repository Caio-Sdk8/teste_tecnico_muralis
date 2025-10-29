using teste_tecnico.Data;
using teste_tecnico.Integrations;
using teste_tecnico.Interfaces;
using teste_tecnico.Interfaces.Services;
using teste_tecnico.Profiles;
using teste_tecnico.Repositories;
using teste_tecnico.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

builder.Services.AddHttpClient<IViaCepService, ViaCepService>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br/");
});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
