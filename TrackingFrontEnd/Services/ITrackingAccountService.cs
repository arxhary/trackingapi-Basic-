namespace TrackingFrontEnd.Services
{
    public interface ITrackingAccountService
    {
        Task<string> GetToken(string clientId, string clientSecret);
    }
}
