using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Salesforce.Common.Models.Json;

namespace Salesforce.Common
{
    public class AuthenticationClient : IAuthenticationClient, IDisposable
    {
        public string InstanceUrl { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Id { get; set; }
        public string ApiVersion { get; set; }

        private const string UserAgent = "forcedotcom-toolkit-dotnet";
        private readonly HttpClient _httpClient;

        private readonly bool _disposeHttpClient;
        private readonly string _domain;
        private readonly string _tokenRequestEndpointUrl;

        public AuthenticationClient(string domain, string apiVersion = "v36.0")
            : this(new HttpClient(), apiVersion)
        {
            _domain = "https://" + domain;
            _tokenRequestEndpointUrl = "https://" + domain + "/services/oauth2/token";
        }

        public AuthenticationClient(HttpClient httpClient, string apiVersion = "v36.0", bool callerWillDisposeHttpClient = false)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");

            _httpClient = httpClient;
            _disposeHttpClient = !callerWillDisposeHttpClient;
            ApiVersion = apiVersion;
        }

        public Task UsernamePasswordAsync(string clientId, string clientSecret, string username, string password)
        {
            return UsernamePasswordAsync(clientId, clientSecret, username, password, _tokenRequestEndpointUrl);
        }

        public async Task UsernamePasswordAsync(string clientId, string clientSecret, string username, string password, string tokenRequestEndpointUrl)
        {
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("clientId");
            if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentNullException("clientSecret");
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
            if (string.IsNullOrEmpty(tokenRequestEndpointUrl)) throw new ArgumentNullException("tokenRequestEndpointUrl");
            if (!Uri.IsWellFormedUriString(tokenRequestEndpointUrl, UriKind.Absolute)) throw new FormatException("tokenRequestEndpointUrl");

            var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(tokenRequestEndpointUrl),
                Content = content
            };

            request.Headers.UserAgent.ParseAdd(string.Concat(UserAgent, "/", ApiVersion));

            var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var authToken = JsonConvert.DeserializeObject<AuthToken>(response);

                AccessToken = authToken.AccessToken;
                InstanceUrl = authToken.InstanceUrl;
                Id = authToken.Id;
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<AuthErrorResponse>(response);
                throw new ForceAuthException(errorResponse.Error, errorResponse.ErrorDescription, responseMessage.StatusCode);
            }
        }

        public Task WebServerAsync(string clientId, string clientSecret, string redirectUri, string code)
        {
            return WebServerAsync(clientId, clientSecret, redirectUri, code, _tokenRequestEndpointUrl);
        }

        public async Task WebServerAsync(string clientId, string clientSecret, string redirectUri, string code, string tokenRequestEndpointUrl)
        {
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("clientId");
            if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentNullException("clientSecret");
            if (string.IsNullOrEmpty(redirectUri)) throw new ArgumentNullException("redirectUri");
            if (!Uri.IsWellFormedUriString(redirectUri, UriKind.Absolute)) throw new FormatException("redirectUri");
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");
            if (string.IsNullOrEmpty(tokenRequestEndpointUrl)) throw new ArgumentNullException("tokenRequestEndpointUrl");
            if (!Uri.IsWellFormedUriString(tokenRequestEndpointUrl, UriKind.Absolute)) throw new FormatException("tokenRequestEndpointUrl");

            var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                    new KeyValuePair<string, string>("code", code)
                });

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(tokenRequestEndpointUrl),
                Content = content
            };

            request.Headers.UserAgent.ParseAdd(string.Concat(UserAgent, "/", ApiVersion));

            var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var authToken = JsonConvert.DeserializeObject<AuthToken>(response);

                AccessToken = authToken.AccessToken;
                InstanceUrl = authToken.InstanceUrl;
                Id = authToken.Id;
                RefreshToken = authToken.RefreshToken;
            }
            else
            {
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<AuthErrorResponse>(response);
                    throw new ForceAuthException(errorResponse.Error, errorResponse.ErrorDescription);
                }
                catch (Exception ex)
                {
                    throw new ForceAuthException(Error.UnknownException, ex.Message + " Raw response: " + response, ex);
                }

            }
        }

        public async Task JwtBearerAsync(string clientId, string user, string pfxPathName, string pfxPassword = "")
        {
            var claims = new Dictionary<string, string>()
            {
                { "iss", clientId },
                { "aud", _domain },
                { "sub", user },
                { "exp", DateTimeOffset.Now.AddMinutes(3).ToUnixTimeSeconds().ToString() }
            };

            var cert = new X509Certificate2(pfxPathName, pfxPassword);
            var key = cert.GetRSAPrivateKey();

            var encodedToken = Jose.JWT.Encode(claims, key, Jose.JwsAlgorithm.RS256);

            var content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer"),
                new KeyValuePair<string, string>("assertion", encodedToken)
            });

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_tokenRequestEndpointUrl),
                Content = content
            };

            var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var authToken = JsonConvert.DeserializeObject<AuthToken>(response);

                AccessToken = authToken.AccessToken;
                InstanceUrl = authToken.InstanceUrl;
                Id = authToken.Id;
                RefreshToken = authToken.RefreshToken;
            }
            else
            {
                try
                {
                    var errorResponse = JsonConvert.DeserializeObject<AuthErrorResponse>(response);
                    throw new ForceAuthException(errorResponse.Error, errorResponse.ErrorDescription);
                }
                catch (Exception ex)
                {
                    throw new ForceAuthException(Error.UnknownException, ex.Message + " Raw response: " + response, ex);
                }

            }
        }

        public Task TokenRefreshAsync(string clientId, string refreshToken, string clientSecret = "")
        {
            return TokenRefreshAsync(clientId, refreshToken, clientSecret, _tokenRequestEndpointUrl);
        }

        public async Task TokenRefreshAsync(string clientId, string refreshToken, string clientSecret, string tokenRequestEndpointUrl)
        {
            var url = Common.FormatRefreshTokenUrl(
                tokenRequestEndpointUrl,
                clientId,
                refreshToken,
                clientSecret);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url)
            };

            request.Headers.UserAgent.ParseAdd(string.Concat(UserAgent, "/", ApiVersion));

            var responseMessage = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var authToken = JsonConvert.DeserializeObject<AuthToken>(response);

                AccessToken = authToken.AccessToken;
                RefreshToken = refreshToken;
                InstanceUrl = authToken.InstanceUrl;
                Id = authToken.Id;
            }
            else
            {
                var errorResponse = JsonConvert.DeserializeObject<AuthErrorResponse>(response);
                throw new ForceException(errorResponse.Error, errorResponse.ErrorDescription);
            }
        }


        public async Task GetLatestVersionAsync()
        {
            try
            {
                string serviceURL = InstanceUrl + @"/services/data/";
                HttpResponseMessage responseMessage = await _httpClient.GetAsync(serviceURL);

                var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    try
                    {
                        var jToken = JToken.Parse(response);
                        if (jToken.Type == JTokenType.Array)
                        {
                            var jArray = JArray.Parse(response);
                            List<Models.Json.Version> versionList = JsonConvert.DeserializeObject<List<Models.Json.Version>>(jArray.ToString());
                            if (versionList != null && versionList.Count > 0)
                            {
                                versionList.Sort();
                                if (!string.IsNullOrEmpty(versionList.Last().version))
                                    ApiVersion = "v" + versionList.Last().version;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new ForceException(Error.UnknownException, e.Message, e);
                    }
                }
                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<AuthErrorResponse>(response);
                        throw new ForceException(errorResponse.Error, errorResponse.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                throw new ForceAuthException(Error.UnknownException, ex.Message, ex);
            }
        }

        public void Dispose()
        {
            if (_disposeHttpClient)
            {
                _httpClient.Dispose();
            }
        }
    }
}
