namespace Algenic.Queries.AllScorePolicies
{
    public class ScorePolicyDto
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public ScorePolicyDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
