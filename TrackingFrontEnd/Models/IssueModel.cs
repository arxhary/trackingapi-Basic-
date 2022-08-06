namespace TrackingFrontEnd.Models
{
    public class IssueModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public IssueType IssueType { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }
    }
    public enum Priority
    {
        Low, Meduim, High
    }

    public enum IssueType
    {
        Feature, Bug, Documentation
    }
}
