using System;
using System.Collections.Generic;

namespace WeddingCD.DAL.Entities
{
    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            this.Pictures = new List<Picture>();
        }

        public long IdPerson { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}