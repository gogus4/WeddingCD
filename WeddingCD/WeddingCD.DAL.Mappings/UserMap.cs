using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeddingCD.DAL.Entities;

namespace WeddingCD.DAL.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUser);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.IdUser).HasColumnName("IdUser");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
        }
    }
}