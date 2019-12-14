namespace Algenic.Queries.ContestOwner
{
    public class ContestOwnerResult
    {
        public bool IsOwner { get; }

        public ContestOwnerResult(bool isOwner)
        {
            IsOwner = isOwner;
        }
    }
}
