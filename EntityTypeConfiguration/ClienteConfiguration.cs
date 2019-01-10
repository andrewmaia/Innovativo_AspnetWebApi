using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class ClienteConfiguration : IEntityTypeConfiguration<Cliente> {

    public void Configure(EntityTypeBuilder<Cliente> builder){
        builder.ToTable("cliente").HasKey(c => c.ID);                
    }
  }
}
