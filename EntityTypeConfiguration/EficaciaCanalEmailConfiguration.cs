using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalEmailConfiguration : IEntityTypeConfiguration<EficaciaCanalEmail> {

    public void Configure(EntityTypeBuilder<EficaciaCanalEmail> builder){
        builder.ToTable("EficaciaCanalEmail").HasKey(ece=> ece.ID);                  
        builder.ToTable("EficaciaCanalEmail")
                .HasOne(ece => ece.EficaciaCanaisRelatorio)
                .WithOne(ecr=> ecr.Email);
    }
  }
}
                
