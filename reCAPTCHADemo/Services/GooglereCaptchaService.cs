using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Net.Http.Json;
using System.Threading.Tasks;


namespace reCAPTCHADemo.Services
{
    public class GooglereCaptchaService
    {
        HttpClient httpClient = new HttpClient();
        public GooglereCaptchaService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        private ReCaptchaSettings _settings = new ReCaptchaSettings();

        public virtual async Task<GoogleREspo> reVerify(string _Token)
        {
            GooglereCaptchaData _MyData = new GooglereCaptchaData { response = _Token, secret = _settings.secretKey };

            GoogleREspo _GoogleREspo = new GoogleREspo();
            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("secret", _settings.secretKey), new KeyValuePair<string, string>("response", _Token) });
            var response = await httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify", content);

            var jsonString = await response.Content.ReadAsStringAsync();
            var capresponse = JsonConvert.DeserializeObject<GoogleREspo>(jsonString);
            return capresponse;
        }
    }

    public class GooglereCaptchaData
    {
        public string response { get; set; } //tocken
        public string secret { get; set; }
    }

    public class GoogleREspo
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }

    public class ReCaptchaSettings
    {
        public string siteKey { get; set; } = "6Lc9bL8aAAAAAEEswtv_7qUS0ZPCxujMThETm_W2";
        public string secretKey { get; set; } = "6Lc9bL8aAAAAAKWWoY07Tidrhbq4ca6uxI-EpB3i";

    }
}



