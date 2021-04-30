using Ch7.Scripts;
using UnityEngine;

namespace Ch2.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private AudioClip hurtSound;
        private AudioSource _playerSounds;


        private void Awake()
        {
            Messenger.AddListener(GameEvent.HealthChanged, OnHealthChanged);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.HealthChanged, OnHealthChanged);
        }

        private void OnHealthChanged()
        {
            _playerSounds.PlayOneShot(hurtSound);
        }

        private void Start()
        {
            _playerSounds = GetComponent<AudioSource>();
        }
    }
}