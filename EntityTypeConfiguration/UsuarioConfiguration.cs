using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario> {

    public void Configure(EntityTypeBuilder<Usuario> builder){
        builder.ToTable("usuario").HasKey(c => c.ID);
        builder.ToTable("usuario")
            .HasOne(u => u.Cliente)
            .WithMany(c => c.UsuarioLista)
            .HasForeignKey(u=> u.ClienteID);                        
    }
  }
}
