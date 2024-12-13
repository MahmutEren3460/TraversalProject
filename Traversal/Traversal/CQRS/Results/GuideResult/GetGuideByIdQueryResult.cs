namespace Traversal.CQRS.Results.GuideResult
{
    public class GetGuideByIdQueryResult
    {
        public int GuideID { get; set; }
        public string? Name { get; set; }
        public string? Descrition { get; set; }
        public string? Image { get; set; }
    }
}
