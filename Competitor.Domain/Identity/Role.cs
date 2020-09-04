using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Competitor.Domain.Identity
{
    [Table("Identity_Role")]
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
