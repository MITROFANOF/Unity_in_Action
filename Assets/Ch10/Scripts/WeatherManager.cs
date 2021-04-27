using System;
using System.Collections.Generic;
using System.Xml;
using Ch7.Scripts;
using Ch9.Scripts;
using MiniJSON;
using UnityEngine;

namespace Ch10.Scripts
{
    public class WeatherManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public float CloudValue { get; private set; }
        
        public void Startup()
        {
            Debug.Log("Weather manager starting...");
            StartCoroutine(NetworkService.GetWeatherJson(OnJSONDataLoaded));
            Status = ManagerStatus.Initializing;
        }

        private void OnJSONDataLoaded(string data)
        {
            if (Json.Deserialize(data) is Dictionary<string, object> dict)
            {
                var clouds = (Dictionary<string, object>) dict["clouds"];
                CloudValue = (long) clouds["all"] / 100f;
            }

            Debug.Log("Value: " + CloudValue);
            Messenger.Broadcast(GameEvent.WeatherUpdated);
            Status = ManagerStatus.Started;
        }

        // ReSharper disable once UnusedMember.Local
        private void OnXMLDataLoaded(string data)
        {
            var doc = new XmlDocument();
            doc.LoadXml(data);
            XmlNode root = doc.DocumentElement;
            var node = root?.SelectSingleNode("clouds");
            var value = node?.Attributes?["value"].Value;
            CloudValue = Convert.ToInt32(value) / 100f;
            Debug.Log("Value: " + CloudValue);
            Messenger.Broadcast(GameEvent.WeatherUpdated);
            Status = ManagerStatus.Started;
            
        }
        
    }
}
