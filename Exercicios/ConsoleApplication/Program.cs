using System;
using System.Diagnostics;
using System.Json;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://accounts.google.com/o/oauth2/auth";
            string responseCode = "code";
            string clientId = "310647620794-fac9676bn3tkd950i7jl6nh688dtfck3.apps.googleusercontent.com";
            string redirectUri = "urn:ietf:wg:oauth:2.0:oob";
            string scope = HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly");
            string clientSecret = "ZQ_3fqP6-mHUeLfHxbh_gqN1";
            string grantType = "authorization_code";
            string tokenUrl = "https://accounts.google.com/o/oauth2/token";

            var finalUrl = string.Format("{0}?response_type={1}&client_id={2}&redirect_uri={3}&scope={4}", url, responseCode,
                                    clientId, HttpUtility.UrlEncode(redirectUri), scope);

            var process = Process.Start("iexplore.exe", finalUrl);

            Thread.Sleep(10000);
            string title;
            string code;

            if (process != null && !process.HasExited)
            {
                title = process.MainWindowTitle;
                var splitResult = title.Split(new char[] { '=', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                code = splitResult[2];
            }
            else
            {
                code = Console.ReadLine();
            }

            Dictionary<string, string> buffer = new Dictionary<string, string>
                                                        {
                                                            {"code", code},
                                                            {"redirect_uri", redirectUri},
                                                            {"grant_type", grantType},
                                                            {"client_id", clientId},
                                                            {"client_secret", clientSecret}
                                                        };

            HttpClient client = new HttpClient();
            FormUrlEncodedContent content = new FormUrlEncodedContent(buffer);
            var result = client.PostAsync(tokenUrl, content).Result;

            JsonValue value = JsonValue.Parse(result.Content.ReadAsStringAsync().Result);

            var restUri = "https://www.googleapis.com/tasks/v1/users/@me/lists";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(value.GetValue("token_type").ReadAs<string>(), value.GetValue("access_token").ReadAs<string>());
            var r = client.GetAsync(restUri).Result.Content.ReadAsStringAsync().Result;
        }
    }
}
