using UnityEngine;
using UnityEngine.UI;

namespace Ch7.Scripts
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Slider speedSlider;
        [SerializeField] private Slider musicSlider;

        private void Start()
        {
            speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
            musicSlider.value = PlayerPrefs.GetFloat("music", 0.2f);
        }

        public void Open()
        {  
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void OnSubmitName(string textName)
        {
            
        }

        public void OnSpeedValue(float speed)
        {
            Messenger<float>.Broadcast(GameEvent.SpeedChanged, speed);
        }

        public void OnMusicValue(float music)
        {
            Messenger<float>.Broadcast(GameEvent.MusicChanged, music);
        }
    }
}
