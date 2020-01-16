namespace Algenic.Queries.AllUsers
{
    public class UserDto
    {
        public string Id { get; }
        public string Name { get; }
        public bool IsExaminer { get; set; }

        public UserDto(string id, string name, bool isExaminer)
        {
            Id = id;
            Name = name;
            IsExaminer = isExaminer;
        }
    }
}
