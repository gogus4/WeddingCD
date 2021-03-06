using System;
using System.Collections.Generic;

namespace WeddingCD.DAL.Entities
{
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.Pictures = new List<Picture>();
        }

        public long IdCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}