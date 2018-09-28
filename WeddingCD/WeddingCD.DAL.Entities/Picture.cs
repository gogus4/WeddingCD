using System;
using System.Collections.Generic;

namespace WeddingCD.DAL.Entities
{
    public partial class Picture
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Picture()
        {
        }

        public long IdPicture { get; set; }
        public long IdCategory { get; set; }
        public long IdPerson { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }
        public virtual Person Person { get; set; }
    }
}