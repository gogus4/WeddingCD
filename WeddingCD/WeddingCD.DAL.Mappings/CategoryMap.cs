using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeddingCD.DAL.Entities;

namespace WeddingCD.DAL.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCategory);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.IdCategory).HasColumnName("IdCategory");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}