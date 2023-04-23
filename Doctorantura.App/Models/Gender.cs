using System.Collections.Generic;

namespace Doctorantura.App.Models
{
    public class Gender
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
