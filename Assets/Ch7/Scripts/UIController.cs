using System.Collections;
using Ch12.Scripts;
using Ch9.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Ch7.Scripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text scoreLabel;
        [SerializeField] private Text healthLabel;
        [SerializeField] private SettingsPopup settingsPopup;
        [SerializeField] private InventoryPopup inventoryPopup;
        [SerializeField] private Text levelEnding;

        private int _score;

        private void Awake()
        {
            Messenger.AddListener(GameEvent.EnemyHit, OnEnemyHit);
            Messenger.AddListener(GameEvent.HealthChanged, OnHealthChanged);
            Messenger.AddListener(GameEvent.LevelCompleted, OnLevelCompleted);
            Messenger.AddListener(GameEvent.LevelFailed, OnLevelFailed);
            Messenger.AddListener(GameEvent.GameCompleted, OnGameCompleted);
        }

        private void OnGameCompleted()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "You finished the game!!!";
        }


        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.EnemyHit, OnEnemyHit);
            Messenger.RemoveListener(GameEvent.HealthChanged, OnHealthChanged);
            Messenger.RemoveListener(GameEvent.LevelCompleted, OnLevelCompleted);
            Messenger.RemoveListener(GameEvent.LevelFailed, OnLevelFailed);
            Messenger.RemoveListener(GameEvent.GameCompleted, OnGameCompleted);
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
            OnHealthChanged();
            settingsPopup.Close();
            inventoryPopup.gameObject.SetActive(false);
            levelEnding.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.M)) return;
            var isShowing = inventoryPopup.gameObject.activeSelf;
            inventoryPopup.gameObject.SetActive(!isShowing);
            inventoryPopup.Refresh();
        }

        public void OnOpenSettings()
        {
            settingsPopup.Open();
        }

        private void OnHealthChanged()
        {
            var healthString = $"Health: {Managers.Player.Health} / {Managers.Player.MaxHealth}";
            healthLabel.text = healthString;
        }

        private void OnLevelCompleted()
        {
            StartCoroutine(CompleteLevel());
        }

        private IEnumerator CompleteLevel()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "Level Complete!";
            yield return new WaitForSeconds(2f);

            Managers.Misson.GoToNext();
        }

        private void OnLevelFailed()
        {
            StartCoroutine(FailLevel());
        }

        private IEnumerator FailLevel()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "Level Failed";

            yield return new WaitForSeconds(2f);

            Managers.Player.Respawn();
            Managers.Misson.LoadLevel();
        }

        public void SaveGame()
        {
            Managers.Data.SaveGameState();
        }

        public void LoadGame()
        {
            Managers.Data.LoadGameState();
        }
    }
}