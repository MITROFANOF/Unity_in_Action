using Ch7.Scripts;
using Ch9.Scripts;
using UnityEngine;

namespace Ch11.Scripts
{
    public class AudioManager : MonoBehaviour, IGameManager
    {

        [SerializeField] private AudioSource music;
        
        public ManagerStatus Status { get; private set; }

        private void Awake()
        {
            Messenger<float>.AddListener(GameEvent.MusicChanged, OnMusicChange);
        }

        private void OnDestroy()
        {
            Messenger<float>.RemoveListener(GameEvent.MusicChanged, OnMusicChange);
        }

        private static void OnMusicChange(float value)
        {
            Managers.Audio.music.volume = value;
        }

        public void Startup()
        {
            Debug.Log("Audio manager starting...");

            
            Status = ManagerStatus.Started;
        }
    }
}
