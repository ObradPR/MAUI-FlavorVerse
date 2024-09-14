using FlavorVerse.Extensions;
using RestSharp;

namespace FlavorVerse
{
    public static class Api
    {
        private static RestClient _client;
        private static bool authorizationSet = false;

        public static string BaseUrl => "https://localhost:7065/api/";

        public static RestClient Client
        {
            get
            {
                _client ??= new RestClient(BaseUrl);

                var user = SecureStorage.Default.GetUser();

                if (user != null && !authorizationSet)
                {
                    authorizationSet = true;
                    _client.AddDefaultHeader("Authorization", "Bearer " + user.Token);
                }

                return _client;
            }
        }
    }
}