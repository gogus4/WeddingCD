using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeddingCD.DAL.Entities;

namespace WeddingCD.DAL.Mappings
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPerson);

            // Table & Column Mappings
            this.ToTable("Person");
            this.Property(t => t.IdPerson).HasColumnName("IdPerson");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}