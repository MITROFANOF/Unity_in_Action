using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ch7.Scripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text scoreLabel;
        [SerializeField] private SettingsPopup settingsPopup;
        private int _score;

        private void Awake()
        {
            Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        }

        private void OnEnemyHit()
        {
            _score += 1;
            scoreLabel.text = _score.ToString();
        }

        private void Start()
        {
            _score = 0;
            scoreLabel.text = _score.ToString();
            
            settingsPopup.Close();
        }


        public void OnOpenSettings()
        {
            settingsPopup.Open();
        }
        
    }
}
