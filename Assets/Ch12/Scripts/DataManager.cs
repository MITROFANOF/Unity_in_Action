using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ch9.Scripts;
using UnityEngine;

namespace Ch12.Scripts
{
    public class DataManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        private string _filename;

        public void Startup()
        {
            Debug.Log("Data manager starting...");

            _filename = Path.Combine(Application.persistentDataPath, "game.dat");
            Status = ManagerStatus.Started;
        }

        public void SaveGameState()
        {
            var gamestate = new Dictionary<string, object>
            {
                {"inventory", Managers.Inventory.GetData()},
                {"health", Managers.Player.Health},
                {"maxHealth", Managers.Player.MaxHealth},
                {"currentLevel", Managers.Misson.CurrentLevel},
                {"maxLevel", Managers.Misson.MaxLevel}
            };

            var stream = File.Create(_filename);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, gamestate);
            stream.Close();
        }

        public void LoadGameState()
        {
            if (!File.Exists(_filename))
            {
                Debug.Log("No saved games");
                return;
            }

            var formatter = new BinaryFormatter();
            var stream = File.Open(_filename, FileMode.Open);
            var gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
            stream.Close();


            if (gamestate == null)
                throw new FileLoadException("File load error");
            
            Managers.Inventory.UpdateData((Dictionary<string, int>) gamestate["inventory"]);
            Managers.Player.UpdateData((int) gamestate["health"], (int) gamestate["maxHealth"]);
            Managers.Misson.UpdateData((int) gamestate["currentLevel"], (int) gamestate["maxLevel"]);


            Managers.Misson.LoadLevel();
        }
    }
}