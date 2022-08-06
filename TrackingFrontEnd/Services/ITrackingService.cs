using TrackingFrontEnd.Models;

namespace TrackingFrontEnd.Services
{
    public interface ITrackingService
    {
        Task<IEnumerable<IssueModel>> GetAsync(string id);
    }
}
