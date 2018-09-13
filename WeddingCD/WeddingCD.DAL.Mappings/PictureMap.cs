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
            //this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.AddBy).HasColumnName("AddBy");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Pictures)
                .HasForeignKey(d => d.IdCategory);
        }
    }
}