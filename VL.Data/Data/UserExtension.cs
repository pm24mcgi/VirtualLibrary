//using Microsoft.AspNetCore.Identity;

//namespace VL.Shared.Data
//{
//    public class UserExtension : IdentityUser
//    {
//        public string? FirstName { get; set; }
//        public string? LastName { get; set; }
//    }
//}

// This is how we could extend the IdentityUser built in class to include additional fields.
// Not implemented in current migration or Razor pages project

// If implementing, would need to pass this class to this ApplicationDbContext class with a tag:
// public class ApplicationDbContext : IdentityDbContext<UserExtension>

// Would also need to alter DI container:
// builder.Services.AddIdentity<UserExtension, IdentityRole>()

// revisit this