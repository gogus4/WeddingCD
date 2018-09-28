using System;
using System.Collections.Generic;

namespace WeddingCD.DAL.Entities
{
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
        }

        public long IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}