namespace Algenic.FunctionalTests.Setup
{
    public class PredefinedUsers
    {
        public UserCredentials RegularUser { get; set; } = new UserCredentials();
        public UserCredentials Examiner { get; set; } = new UserCredentials();
        public UserCredentials Admin { get; set; } = new UserCredentials();
    }
}
