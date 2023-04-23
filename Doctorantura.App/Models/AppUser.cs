using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Doctorantura.App.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(75)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [StringLength(75)]
        [MinLength(3)]
        public string Surname { get; set; }

        [Required]
        [StringLength(75)]
        [MinLength(3)]
        public string FatherName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DataType Born { get; set; }

        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public virtual ICollection<CalcTask> CalcTasks { get; set; }

        public string Image { get; set; }
    }
}