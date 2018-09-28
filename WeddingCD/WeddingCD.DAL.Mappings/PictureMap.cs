using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeddingCD.DAL.Entities;

namespace WeddingCD.DAL.Mappings
{
    public class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPicture);

            // Table & Column Mappings
            this.ToTable("Picture");
            this.Property(t => t.IdPicture).HasColumnName("IdPicture");
            this.Property(t => t.IdCategory).HasColumnName("IdCategory");
            this.Property(t => t.Path).HasColumnName("Path");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Pictures)
                .HasForeignKey(d => d.IdCategory);

            this.HasRequired(t => t.Person)
                .WithMany(t => t.Pictures)
                .HasForeignKey(d => d.IdPerson);
        }
    }
}