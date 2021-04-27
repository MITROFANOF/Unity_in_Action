using Ch7.Scripts;
using Ch9.Scripts;
using UnityEngine;

namespace Ch10.Scripts
{
    public class WeatherController : MonoBehaviour
    {
        [SerializeField] private Material sky;
        [SerializeField] private Light sun;

        private float _fullIntensity;
        private static readonly int Blend = Shader.PropertyToID("_Blend");

        private void Awake()
        {
            Messenger.AddListener(GameEvent.WeatherUpdated, OnWeatherUpdated);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.WeatherUpdated, OnWeatherUpdated);
        }

        private void OnWeatherUpdated()
        {
            SetOvercast(Managers.Weather.CloudValue);
        }

        private void Start()
        {
            _fullIntensity = sun.intensity;
        }

        private void SetOvercast(float value)
        {
            sky.SetFloat(Blend, value);
            sun.intensity = _fullIntensity - (_fullIntensity * value);
        }
    }
}
