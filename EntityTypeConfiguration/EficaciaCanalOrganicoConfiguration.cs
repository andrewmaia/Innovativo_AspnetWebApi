using Microsoft.EntityFrameworkCore;
using Innovativo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovativo.EntityTypeConfiguration {
  public class EficaciaCanalOrganicoConfiguration : IEntityTypeConfiguration<EficaciaCanalOrganico> {

    public void Configure(EntityTypeBuilder<EficaciaCanalOrganico> builder){
        builder.ToTable("EficaciaCanalOrganico").HasKey(eco=> eco.ID);                        
        builder.ToTable("EficaciaCanalOrganico")
                .HasOne(eco => eco.EficaciaCanaisRelatorio)
                .WithOne(ecr=> ecr.Organico);
    }
  }
}
                
