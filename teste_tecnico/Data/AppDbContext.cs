using Microsoft.EntityFrameworkCore;
using teste_tecnico.Domains;

namespace teste_tecnico.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Contato> Contatos => Set<Contato>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TesteMuralis.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Endereco)
            .WithOne(e => e.Cliente)
            .HasForeignKey<Endereco>(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Contatos)
            .WithOne(ct => ct.Cliente)
            .HasForeignKey(ct => ct.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Endereco>()
            .Property(e => e.Cep)
            .IsRequired()
            .HasMaxLength(10);

        modelBuilder.Entity<Contato>()
            .Property(ct => ct.Tipo)
            .IsRequired()
            .HasMaxLength(100);
    }
}
