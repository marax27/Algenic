using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;

namespace Algenic.Queries.AllUsers
{
    public class AllUsersQuery
    {
        private AllUsersQuery() { }

        public static AllUsersQuery Create()
            => new AllUsersQuery();
    }

    public class AllUsersResult
    {
        public IEnumerable<UserDto> UserDtos { get; }

        public AllUsersResult(IEnumerable<UserDto> userDtos)
            => UserDtos = userDtos;
    }

    public class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, AllUsersResult>
    {
        private ApplicationDbContext _dbContext;

        public AllUsersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AllUsersResult> HandleAsync(AllUsersQuery query)
        {
            var examinerRoleId = _dbContext.Roles.Single(r => r.Name == "Examiner").Id;
            var adminRoleId = _dbContext.Roles.Single(r => r.Name == "Admin").Id;


            var dtos = _dbContext.Users
                .Select(user => new UserDto(
                    user.Id,
                    user.UserName,
                    false
                )).ToList();
            foreach (var dto in dtos)
                dto.IsExaminer = IsInRole(dto.Id, examinerRoleId);
            return new AllUsersResult(dtos.Where(dto => !IsInRole(dto.Id, adminRoleId)));
        }

        private bool IsInRole(string userId, string roleId)
        {
            return _dbContext.UserRoles
                .Any(x => x.UserId == userId && x.RoleId == roleId);
        }
    }
}
