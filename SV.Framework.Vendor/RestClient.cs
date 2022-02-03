using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace SV.Framework.Vendor
{
	public class RestClient : IDisposable
	{
		private bool disposed;

		private HttpClient _client;
		private HttpResponseMessage responseMessage;
		private TimeSpan _timeout = TimeSpan.FromMilliseconds(300000);

		public RestClient(string baseUrl)
		{
			_client = new HttpClient () {
				BaseAddress = new Uri (baseUrl)
			};
		}

		~RestClient()
		{
			Dispose(false);
		}

		public HttpResponseMessage ResponseMessage
		{
			get {
				return responseMessage;
			}

		}

		public async Task<T> GetAsync<T>(string url, long timeoutInMS = 300000)
		{
			_timeout = TimeSpan.FromMilliseconds (timeoutInMS);
			return await GetAsync<T>(url, null);
		}

		public async Task<string> GetAsyncJson(string url)
		{
			return await GetAsyncJson(url, null);
		}

		public async Task<string> GetAsyncJson(string url, long timeoutInMS)
		{
			_timeout = TimeSpan.FromMilliseconds(timeoutInMS); 
			return await GetAsyncJson(url, null);
		}

		public async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers)
		{
			T result = default(T);
	
			// Debug.WriteLine("Get request with URL: {0}", _client.BaseAddress.AbsoluteUri);
			string json = await GetAsyncJson(url, headers);
			if (!string.IsNullOrWhiteSpace(json)  && responseMessage.IsSuccessStatusCode ) {
				result = JsonConvert.DeserializeObject<T> (json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
			}

			return result;
		}

		public async Task<string> GetAsyncJson(string url, Dictionary<string, string> headers)
		{
			string json = string.Empty;

			var request = new HttpRequestMessage(HttpMethod.Get, url);

			if (headers != null)
			{
				foreach (KeyValuePair<string, string> entry in headers)
				{
					request.Headers.Add(entry.Key, entry.Value);
				}
			}

			try {

				_client.Timeout = _timeout;
				responseMessage = await _client.SendAsync(request).ConfigureAwait(false);
			
				if (responseMessage.IsSuccessStatusCode)
				{
                    if (responseMessage.Content != null)
                    {
                        json = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
					else
					{
						return "HTTP Response Content is null.";
					}
				}
				else
				{
					// Debug.WriteLineTTP Error, status code: {0}", response.StatusCode);
					return responseMessage.StatusCode.ToString();
				}
			} catch (Exception ex) {
				throw ex;
			}

			return json;
		}

		public async Task<TResult> PostAsync<TResult, T>(T data, string url)
		{
			return await PostAsync<TResult, T>(data, url, null);
		}

		public async Task<TResult> PostAsync<TResult, T>(T data, string url, Dictionary<string, string> headers)
		{
			// Debug.WriteLineost request with URL: {0}", _client.BaseAddress.AbsoluteUri);

			TResult result = default(TResult);

			var request = new HttpRequestMessage(HttpMethod.Post, url);

			if (headers != null)
			{
				foreach (KeyValuePair<string, string> entry in headers)
				{
					request.Headers.Add(entry.Key, entry.Value);
				}
			}

			_client.Timeout = _timeout;
			request.Content = await Task.Factory.StartNew(() => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).ConfigureAwait(false);
			responseMessage = await _client.SendAsync(request).ConfigureAwait(false);
			if (responseMessage.IsSuccessStatusCode)
			{
				if (responseMessage.Content != null)
				{
					var settings = new JsonSerializerSettings
					{
						NullValueHandling = NullValueHandling.Ignore,
						MissingMemberHandling = MissingMemberHandling.Ignore
					};
					//var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateFormatHandling = DateFormatHandling.IsoDateFormat };

					string content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
					//// Debug.WriteLineerialized response: {0}", content);
					result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TResult>(content)).ConfigureAwait(false);
				}
				else
				{
					// Debug.WriteLineTTP Response Content is null.");
				}
			}
			else
			{
				// Debug.WriteLineTTP Error, status code: {0}", response.StatusCode);
			}
			return result;
		}

        public async Task PostAsync(string url, Dictionary<string, string> headers)
		{
			// Debug.WriteLineost request with URL: {0}", _client.BaseAddress.AbsoluteUri);

			
			var request = new HttpRequestMessage(HttpMethod.Post, url);

			if (headers != null)
			{
				foreach (KeyValuePair<string, string> entry in headers)
				{
					request.Headers.Add(entry.Key, entry.Value);
				}
			}

			_client.Timeout = _timeout;

            responseMessage = await _client.SendAsync(request).ConfigureAwait(false);

			
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				if (_client != null)
				{
					_client.Dispose();
					_client = null;
				}
			}

			disposed = true;
		}

		public static string GetUrlFriendlyDate(DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd");
		}

		public static KeyValuePair<string, string> BuildBasicAuthorizationHeader(string username, string password)
		{
			string toEncode = string.Format("{0}:{1}", username, password);
			return new KeyValuePair<string, string>("Authorization", string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(toEncode))));
		}
	}
}

