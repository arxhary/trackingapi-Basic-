using TrackingFrontEnd.Models;

namespace TrackingFrontEnd.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly HttpClient _httpClient;

        public TrackingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<IEnumerable<IssueModel>> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
