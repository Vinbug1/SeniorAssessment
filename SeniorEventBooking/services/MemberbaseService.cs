public class MemberbaseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public MemberbaseService(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_config["MemberbaseApi:BaseUrl"])
        };
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config["MemberbaseApi:ApiKey"]}");
    }

    public async Task<HttpResponseMessage> CreateMemberAsync(object member)
    {
        return await _httpClient.PostAsJsonAsync(_config["MemberbaseEndpoints:Members"], member);
    }

    public async Task<HttpResponseMessage> CreateBookingAsync(object booking)
    {
        return await _httpClient.PostAsJsonAsync(_config["MemberbaseEndpoints:Bookings"], booking);
    }

    public async Task<HttpResponseMessage> CreateEventAsync(object evt)
    {
        return await _httpClient.PostAsJsonAsync(_config["MemberbaseEndpoints:Events"], evt);
    }
}
