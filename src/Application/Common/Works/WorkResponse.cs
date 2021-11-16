namespace Application.Common.Works
{
    public sealed class WorkResponse
    {
        public WorkResponse(WorkDto? item)
        {
            Item = item;
            Success = item != null;
        }
        public bool Success { get; }
        public WorkDto? Item { get; }
    }
}
