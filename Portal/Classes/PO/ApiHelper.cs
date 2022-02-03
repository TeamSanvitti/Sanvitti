using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Configuration;

namespace avii.Classes
{
    public static class ApiHelper
    {
        private static KeyValuePair<string, string> _basicAuthorizationHeader;
		private static HttpResponseMessage responseMessage;
        
		public static KeyValuePair<string, string> BasicAuthorizationHeader
        {
            get
            {
                if (_basicAuthorizationHeader.Equals(default(KeyValuePair<string, string>)))
                {
                    string username = "";// Configuration.PublicApiUsername;
                    string password = "";// Configuration.PublicApiPassword;
                    _basicAuthorizationHeader = RestClient.BuildBasicAuthorizationHeader(username, password);
                }

                return _basicAuthorizationHeader;
            }
        }



        public static HttpResponseMessage ResponseMessage
		{
			get {
				return responseMessage;
			}

		}

        private static Dictionary<string, string> _headers;
        public static Dictionary<string, string> Headers
        {
            get
            {
                if (_headers == null)
                {
                    _headers = new Dictionary<string, string>();
                   // _headers.Add(BasicAuthorizationHeader.Key, BasicAuthorizationHeader.Value);
                }

                return _headers;
            }
        }

        public static async Task<string> GetAsyncJson(string url)
        {
            return await GetAsyncJson(url, Headers);
        }

        public static async Task<string> GetAsyncJson(string url, Dictionary<string, string> headers)
        {
            RestClient client = new RestClient(url);
            return await client.GetAsyncJson(url, headers);
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            return await GetAsync<T>(url, Headers);
        }

        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers)
        {
            RestClient client = new RestClient(url);
			var data = await client.GetAsync<T>(url, headers);
			responseMessage = client.ResponseMessage;
			return data;
        }

        public static async Task<TResult> PostAsync<TResult, T>(T data, string url)
        {
            return await PostAsync<TResult, T>(data, url, Headers);
        }

        public static async Task<TResult> PostAsync<TResult, T>(T data, string url, Dictionary<string, string> headers)
        {
            RestClient client = new RestClient(url);
            return await client.PostAsync<TResult, T>(data, url, headers);
        }

		public static async Task PostAsync(string url)
		{
			RestClient client = new RestClient(url);
            await client.PostAsync(url, Headers);
		}

    }
}