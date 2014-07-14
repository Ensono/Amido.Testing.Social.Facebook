using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

using Amido.Testing.Framework;
using Amido.Testing.Social.Facebook.Models;

using RestSharp;

namespace Amido.Testing.Social.Facebook
{
    public class FacebookUserService
    {
        private const string BaseUrl = "https://graph.facebook.com";

        public FacebookUser CreateTestUser(string appId, string appSecret, bool authorised = true)
        {
            var appAccessToken = GetAppAccessToken(appId, appSecret);
            var request = new RestRequest("/{appId}/accounts/test-users", Method.POST);
            request.AddUrlSegment("appId", appId);
            request.AddParameter("installed", authorised.ToString().ToLower());
            request.AddParameter("permissions", "read_stream");
            request.AddParameter("method", "post");
            request.AddParameter("access_token", appAccessToken);

            var restResponse = SendRequest(request);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error creating Facebook test user. HttpStatusCode: " + restResponse.StatusCode + ". Message: " + restResponse.ErrorMessage);
            }

            var facebookUser = JsonSerializer.FromJsonString<FacebookUser>(restResponse.Content);

            return facebookUser;
        }

        public bool Deauthorise(string accessToken)
        {
            var request = new RestRequest("/me/permissions", Method.DELETE);
            request.AddParameter("access_token", accessToken);
            var restResponse = SendRequest(request);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error de-authorising Facebook test user. HttpStatusCode: " + restResponse.StatusCode + ". Message: " + restResponse.ErrorMessage);
            }

            return true;
        }

        private static RestResponse SendRequest(RestRequest request)
        {
            var client = new RestClient { BaseUrl = BaseUrl };

            var response = client.Execute(request);

            return (RestResponse)response;
        }

        private static string GetAppAccessToken(string appId, string appSecret)
        {
            var request = new RestRequest("/oauth/access_token", Method.GET);
            request.AddParameter("client_id", appId);
            request.AddParameter("client_secret", appSecret);
            request.AddParameter("grant_type", "client_credentials");

            var restResponse = SendRequest(request);

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error getting Facebook access token. HttpStatusCode: " + restResponse.StatusCode + ". Message: " + restResponse.ErrorMessage);
            }

            return GetAccessToken(restResponse.Content);
        }

        private static string GetAccessToken(string url)
        {
            var regex = new Regex("access_token=(.+)");
            var match = regex.Match(url);
            var value = match.Groups[0].Value;
            var accessToken = value.Split('=').Skip(1).First();
            return accessToken;
        }
    }
}