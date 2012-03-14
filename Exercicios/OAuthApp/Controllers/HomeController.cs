using System.Json;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Web;
using System.Net.Http;
using System.Collections.Generic;

namespace OAuthApp.Controllers
{
    public class HomeController : Controller
    {
        string url = "https://accounts.google.com/o/oauth2/auth";
        string responseCode = "code";
        string clientId = "310647620794.apps.googleusercontent.com";
        string redirectUri = HttpUtility.UrlEncode("http://localhost:49816");
        string scope = HttpUtility.UrlEncode("https://www.googleapis.com/auth/tasks.readonly");
        string clientSecret = "KhdhG3FpJDTnUqoI-GrB-JgL";
        string grantType = "authorization_code";
        string tokenUrl = "https://accounts.google.com/o/oauth2/token";

        public ActionResult TaskImplicitExample()
        {
            return View();
        }

        public ActionResult AuthCallback()
        {
            return View();
        }

        // GET: /Home/
        public ActionResult Tasks(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Redirect(string.Format("{0}?response_type={1}&client_id={2}&redirect_uri={3}&scope={4}", url, responseCode, clientId, redirectUri, scope));   
            }
            else
            {
                Dictionary<string, string> buffer = new Dictionary<string, string>
                                                        {
                                                            {"code", code},
                                                            {"redirect_uri", "http://localhost:49816"},
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

                return View();
            }
        }
    }
}