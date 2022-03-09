using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace KAMAZ_Smart_Analytics.Data
{
    public class ApiClientContext
    {
        string APP_PATH = "https://localhost:7234/";
        swaggerClient Client;

        static private string Token;
        public ApiClientContext()
        {
            Client = Init();

            if (!CheckAuth())
            {
                Auth();
                Client = Init();
            }
        }

        public swaggerClient GetClient
        {
            get { return Client; }
        }

        private swaggerClient Init()
        {
            HttpClient httpClient = new HttpClient();
            var header = httpClient.DefaultRequestHeaders;

            if (Token != null)
                header.Add("Authorization", $"Bearer {Token}");

            return new swaggerClient(APP_PATH, httpClient);
        }

        public bool CheckAuth()
        {
            Task result = Client.GetResultAsync();
            try
            {
                result.Wait();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void Auth()
        {
            //var result = Client.AuthAsync("123", "123");
            //result.Wait();
            string username = "123";
            string password = "123";

            HttpClient client_ = new HttpClient();
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(APP_PATH != null ? APP_PATH.TrimEnd('/') : "").Append("/api/Auth/Auth?");
            if (username != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("Username") + "=").Append(System.Uri.EscapeDataString(username) + "&");
            }
            if (password != null)
            {
                urlBuilder_.Append(System.Uri.EscapeDataString("Password") + "=").Append(System.Uri.EscapeDataString(username) + "&");
            }
            urlBuilder_.Length--;

            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Content = new System.Net.Http.StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                    request_.Method = new System.Net.Http.HttpMethod("POST");


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);


                    var response_ = client_.Send(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, CancellationToken.None);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }


                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var task = response_.Content == null ? null : response_.Content.ReadAsStringAsync();
                            task.Wait();
                            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(task.Result);
                            if (values["message"] == "Success")
                                Token = values["token"];
                        }
                        else
                        {
                            var task = response_.Content == null ? null : response_.Content.ReadAsStringAsync();
                            task.Wait();
                            var responseData_ = task.Result;
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
            //response_.Content.ReadAsStringAsync().ConfigureAwait(false);

        }

    }
}
