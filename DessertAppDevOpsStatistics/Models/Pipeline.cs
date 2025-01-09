namespace DessertAppDevOpsStatistics.Models
{
    public class Pipeline
    {
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
