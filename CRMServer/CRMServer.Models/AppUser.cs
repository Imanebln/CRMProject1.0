using Microsoft.AspNetCore.Identity;

namespace CRMServer.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}
