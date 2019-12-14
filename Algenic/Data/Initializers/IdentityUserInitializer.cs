using Microsoft.AspNetCore.Identity;

namespace Algenic.Data.Initializers
{
    public static class IdentityUserInitializer
    {
        private static readonly string AdminEmail = "admin@admin";
        private static readonly string AdminPassword = "Paramount71Security";

        // Sample examiner
        private static readonly string ExaminerEmail = "examiner@examiner";
        private static readonly string ExaminerPassword = "Paramount71Security";

        // Sample regular user
        private static readonly string RegularEmail = "regular@regular";
        private static readonly string RegularPassword = "Paramount71Security";

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            SeedUser(userManager, AdminEmail, AdminPassword, "Admin");
            SeedUser(userManager, ExaminerEmail, ExaminerPassword, "Examiner");
            SeedUser(userManager, RegularEmail, RegularPassword);
        }

        private static void SeedUser(UserManager<IdentityUser> userManager, string email, string password, string role=null)
        {
            var locatedUser = userManager.FindByEmailAsync(email).Result;
            if (locatedUser != null)
            {
                return;
            }

            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = userManager.CreateAsync(user, password).Result;
            if (result.Succeeded && role != null)
            {
                userManager.AddToRoleAsync(user, role).Wait();
            }
        }
    }
}
