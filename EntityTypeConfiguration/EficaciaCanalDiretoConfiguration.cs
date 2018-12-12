using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalDiretoConfiguration : IEntityTypeConfiguration<EficaciaCanalDireto> {

    public void Configure(EntityTypeBuilder<EficaciaCanalDireto> builder){
        builder.ToTable("EficaciaCanalDireto").HasKey(ecd=> ecd.ID);            
        builder.ToTable("EficaciaCanalDireto")
                .HasOne(ecd => ecd.EficaciaCanaisRelatorio)
                .WithOne(ecr=> ecr.Direto);
    }
  }
}
                
