using Ch7.Scripts;
using Ch9.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ch12.Scripts
{
    public class MissionManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public int CurrentLevel { get; private set; }
        public int MaxLevel { get; private set; }

        public void Startup()
        {
            Debug.Log("Mission manager starting...");
            UpdateData(0, 3);
            Status = ManagerStatus.Started;
        }

        public void UpdateData(int currentLevel, int maxLevel)
        {
            CurrentLevel = currentLevel;
            MaxLevel = maxLevel;
        }
        
        public void GoToNext()
        {
            if (CurrentLevel < MaxLevel)
            {
                CurrentLevel++;
                LoadLevel();
            }
            else
            {
                Debug.Log("Last level");
                Messenger.Broadcast(GameEvent.GameCompleted);
            }
        }

        public static void ReachObjective()
        {
            Messenger.Broadcast(GameEvent.LevelCompleted);
        }

        public void LoadLevel()
        {
            var levelName = "Level " + CurrentLevel;
            Debug.Log("Loading " + levelName);
            SceneManager.LoadScene(levelName);
        }
    }
}