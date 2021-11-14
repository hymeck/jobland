namespace Application.Queries.Works
{
    public sealed class GetWorkByNameQueryResponse
    {
        public GetWorkByNameQueryResponse(WorkDto? item)
        {
            Item = item;
            Success = item != null;
        }
        public bool Success { get; }
        public WorkDto? Item { get; }
    }
}
