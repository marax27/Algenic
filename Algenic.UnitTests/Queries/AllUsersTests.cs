using System.Linq;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Queries.AllScorePolicies;
using Algenic.Queries.AllUsers;
using Algenic.UnitTests.Setup;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Algenic.UnitTests.Queries
{
    public class AllUsersTests : BaseDatabaseTest
    {
        private IQueryHandler<AllUsersQuery, AllUsersResult> Sut { get; }

        public AllUsersTests()
        {
            Sut = new AllUsersQueryHandler(Context);
        }

        protected override ApplicationDbContext PrepareContext(ApplicationDbContext context)
        {
            foreach (var roleName in new[] {"Admin", "Examiner"})
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                    context.Roles.Add(new IdentityRole(roleName));
            }

            context.SaveChanges();
            return context;
        }

        [Fact]
        public void UserWithExaminerRights_ReturnExpectedUsersData()
        {
            var givenUserName = "sample-user";
            var givenUser = new IdentityUser(givenUserName);

            Context.Users.Add(givenUser);
            Context.SaveChanges();
            ConnectToRole(givenUser, "Examiner");

            var query = AllUsersQuery.Create();
            var result = Sut.HandleAsync(query).Result;
            var actualDto = result.UserDtos.Single(x => x.Id == givenUser.Id);

            actualDto.Name.Should().Be(givenUserName);
            actualDto.IsExaminer.Should().BeTrue();
        }

        private void ConnectToRole(IdentityUser user, string roleName)
        {
            var roleId = Context.Roles.Single(x => x.Name == roleName).Id;
            Context.UserRoles.Add(new IdentityUserRole<string>{RoleId = roleId, UserId = user.Id});
            Context.SaveChanges();
        }
    }
}
