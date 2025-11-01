using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SeniorEventBooking.Services
{
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


        // 📅 Create a booking
        public async Task<HttpResponseMessage> CreateBookingAsync(object booking)
        {
            return await _httpClient.PostAsJsonAsync(_config["MemberbaseEndpoints:Bookings"], booking);
        }


        // 🔍 Check dataset record (GET /datasets/:name/data/:recordId)
        public async Task<HttpResponseMessage> GetDatasetRecordAsync(string datasetName, string recordId)
        {
            string endpoint = _config["MemberbaseEndpoints:Datasets:CheckRecord"]
                .Replace("{name}", datasetName)
                .Replace("{recordId}", recordId);

            return await _httpClient.GetAsync(endpoint);
        }

        // 🆕 Create dataset record (POST /datasets/:name/data)
        public async Task<HttpResponseMessage> CreateDatasetRecordAsync(string datasetName, object data)
        {
            string endpoint = _config["MemberbaseEndpoints:Datasets:CreateRecord"]
                .Replace("{name}", datasetName);

            return await _httpClient.PostAsJsonAsync(endpoint, data);
        }

        // 🎟️ Fetch event tickets (GET /events/:id/tickets)
        public async Task<HttpResponseMessage> GetEventTicketsAsync(string eventId)
        {
            // Build endpoint manually
            string endpoint = $"{_config["MemberbaseEndpoints:Events"]}/{eventId}/tickets";

            return await _httpClient.GetAsync(endpoint);
        }

        // fetching event details
        public async Task<HttpResponseMessage> GetEventDetailsAsync(string eventId)
        {
            string endpoint = $"{_config["MemberbaseEndpoints:Events"]}/{eventId}";
            return await _httpClient.GetAsync(endpoint);
        }

        // fetching all events
        public async Task<HttpResponseMessage> GetAllEventsAsync()
        {
            return await _httpClient.GetAsync(_config["MemberbaseEndpoints:Events"]);
        }
    }
}
