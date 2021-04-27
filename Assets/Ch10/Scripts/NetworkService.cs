using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Ch10.Scripts
{
    public static class NetworkService
    {
        private static readonly string XMLApi = "https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=" + Environment.GetEnvironmentVariable("OWM_TOKEN");

        private static readonly string JsonApi = "https://api.openweathermap.org/data/2.5/weather?q=Chicago,us&APPID=" + Environment.GetEnvironmentVariable("OWM_TOKEN");

        private const string WebImage = "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

        public static IEnumerator DownloadImage(Action<Texture2D> callback)
        {
            var request = UnityWebRequestTexture.GetTexture(WebImage);
            yield return request.SendWebRequest();
            callback(DownloadHandlerTexture.GetContent(request));
        }

        private static IEnumerator CallAPI(string url, Action<string> callback)
        {
            using var request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("network problem: " + request.error);
            }
            else if (request.responseCode != (long) System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("response error: " + request.responseCode);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }

        public static IEnumerator GetWeatherXML(Action<string> callback)
        {
            return CallAPI(XMLApi, callback);
        }
        
        public static IEnumerator GetWeatherJson(Action<string> callback)
        {
            return CallAPI(JsonApi, callback);
        }
    }
}