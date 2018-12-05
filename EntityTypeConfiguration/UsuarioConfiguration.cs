using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario> {

    public void Configure(EntityTypeBuilder<Usuario> builder){
        builder.ToTable("Usuario")
            .HasKey(c => c.ID);                
    }
  }
}
