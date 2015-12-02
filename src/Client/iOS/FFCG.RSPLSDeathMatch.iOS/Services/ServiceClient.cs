using System;
using System.IO;
using System.Net;
using FFCG.RSPLS.DeathMatch.ApiModel;

namespace FFCG.RSPLS.DeathMatch.iOS.Services
{ 
    public class ServiceClient
    {
        private readonly string _baseUri;

        public ServiceClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Game[] GetGames()
        {
            var games = GetResource<Game>("game");
            return games;
        }

        private T[] GetResource<T>(string resource)
        {
            var uri = $"{_baseUri}/api/{resource}";
            var request = new HttpWebRequest(new Uri(uri)) { Method = "GET" };
            try
            {
                var response = request.GetResponse();
                var httpResponse = (HttpWebResponse)response;
                string result;

                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    result = new StreamReader(responseStream).ReadToEnd();
                }
                var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(result);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to download resource", ex);
            }

        }
        private T GetResource<T>(string resource, string id)
        {
            var uri = $"{_baseUri}/api/{resource}/{id}";
            var request = new HttpWebRequest(new Uri(uri)) { Method = "GET" };
            try
            {
                var response = request.GetResponse();
                var httpResponse = (HttpWebResponse)response;
                string result;

                using (Stream responseStream = httpResponse.GetResponseStream())
                {
                    result = new StreamReader(responseStream).ReadToEnd();
                }
                if (id != null)
                {

                }
                var resultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
                return resultObject;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to download resource", ex);
            }

        }
    }
}