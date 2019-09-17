using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WristCare.Model;

namespace WristCare.RequestProvider
{
	public class RequestProvider
	{
		private readonly JsonSerializerSettings _serializerSettings;
		private string _baseAddress;

		public string BaseAddress
		{
			get => _baseAddress;
			set => _baseAddress = value;
		}
		public RequestProvider()
		{
			_serializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				DateTimeZoneHandling = DateTimeZoneHandling.Utc,
				NullValueHandling = NullValueHandling.Ignore
			};
			BaseAddress = "";
			_serializerSettings.Converters.Add(new StringEnumConverter());
		}
		public bool IsSuccess { get; set; }
		// Necessary for connecting with a particular user
		public string ConnectionId { get; set; }
		// Used for accessing the API
		public string AccessToken { get; set; }

		public HttpStatusCode HttpStatusCode { get; set; }

		/// <summary>
		/// Get request that returns aything
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="uri"></param>
		/// <returns></returns>
		public async Task<TResult> GetAsync<TResult>(string uri)
		{
			using (var httpClient = CreateHttpClientAsync(AccessToken))
			{
				var response = await httpClient.GetAsync(uri);

				// Handle response
				IsSuccess = HandleResponse(response);

				if (!IsSuccess)
				{
					// Handle Unauthorized Access
					if (HttpStatusCode == HttpStatusCode.Unauthorized)
						throw new UnauthorizedAccessException("You are not logged in!");
				}
				var serialized =
					await response.Content.ReadAsStringAsync();

				return await Task.Run(() =>
					JsonConvert.DeserializeObject<TResult>(
						serialized, _serializerSettings));
			}
		}

		/// <summary>
		/// A get request that returns nothing
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public async Task GetAsync(string uri)
		{
			using (var httpClient = CreateHttpClientAsync(AccessToken))
			{
				try
				{
					var response = await httpClient.GetAsync(uri);

					// Handle response
					IsSuccess = HandleResponse(response);

					if (!IsSuccess)
					{
						// Handle Unauthorized Access 
						throw new UnauthorizedAccessException("You are not logged in!");
					}

					await response.Content.ReadAsStringAsync();
				}
				catch (Exception)
				{
					// do nothing
				}
			}
		}

		//Only returns the success status code true or false
		public async Task<TResult> PostAsync<TResult>(string uri, TResult data) 
		{
			using (var httpClient = CreateHttpClientAsync(AccessToken))
			{
				var serializedData = JsonConvert.SerializeObject(data);
				var content = new StringContent(serializedData, Encoding.UTF8, "application/json");
				var response = await httpClient.PostAsync(uri, content);
				var serialized = await response.Content.ReadAsStringAsync();
				var result = await Task.Run(() =>
					JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
				return result;
			}
		}

		// returns whether the response is successful or not
		private bool HandleResponse(HttpResponseMessage response)
		{
			if (response.IsSuccessStatusCode) return true;

			if (response.StatusCode == HttpStatusCode.Forbidden ||
				response.StatusCode == HttpStatusCode.Unauthorized ||
				response.StatusCode == HttpStatusCode.BadRequest ||
				response.StatusCode == HttpStatusCode.NotFound)
			{
				HttpStatusCode = response.StatusCode;
				return false;
			}
			return true;
		}

		/// <summary>
		///  Creates a http client
		/// </summary>
		/// <returns></returns>
		private HttpClient CreateHttpClientAsync(string token)
		{
			var httpClient = new HttpClient
			{
				//android base ip 10.0.2.2
				//Dorm ip and house
				BaseAddress = new Uri("http://192.168.1.8/pms/api/")

				//office ip
				//TODO : always check ip CONNECTION
				//BaseAddress = new Uri("http://192.168.143.145/pms/api/")
			};

			httpClient.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			//// Add the connection id of the particular device
			//httpClient.DefaultRequestHeaders.Add("Connection-Id", ConnectionId);

			// Add an access token header
			httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);

			return httpClient;
		}

	}
}
