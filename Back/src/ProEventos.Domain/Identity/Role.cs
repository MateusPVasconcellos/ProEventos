using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProEventos.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
