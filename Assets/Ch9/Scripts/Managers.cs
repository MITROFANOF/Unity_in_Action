using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ch10.Scripts;
using Ch11.Scripts;
using Ch12;
using Ch12.Scripts;
using UnityEngine;

namespace Ch9.Scripts
{
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(WeatherManager))]
    [RequireComponent(typeof(ImagesManager))]
    [RequireComponent(typeof(AudioManager))]
    [RequireComponent(typeof(MissionManager))]
    [RequireComponent(typeof(DataManager))]
    public class Managers : MonoBehaviour
    {
        public static MissionManager Misson { get; private set; }
        public static PlayerManager Player { get; private set; }
        public static InventoryManager Inventory { get; private set; }
        public static WeatherManager Weather { get; private set; }
        public static ImagesManager Images { get; private set; }
        public static AudioManager Audio { get; private set; }
        public static DataManager Data { get; private set; }

        private List<IGameManager> _startSequence;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            Misson = GetComponent<MissionManager>();
            Player = GetComponent<PlayerManager>();
            Inventory = GetComponent<InventoryManager>();
            Weather = GetComponent<WeatherManager>();
            Images = GetComponent<ImagesManager>();
            Audio = GetComponent<AudioManager>();
            Data = GetComponent<DataManager>();
            
            _startSequence = new List<IGameManager> {Misson, Player, Inventory, Images, Audio, Data};

            StartCoroutine(StartupMessages());
        }

        private IEnumerator StartupMessages()
        {
            foreach (var manager in _startSequence)
            {
                manager.Startup();
            }

            yield return null;

            var numModules = _startSequence.Count;
            var numReady = 0;

            while (numReady < numModules)
            {
                var lastReady = numReady;
                numReady = _startSequence.Count(manager => manager.Status == ManagerStatus.Started);
                if(numReady > lastReady)
                {
                    Debug.Log("Progress: " + numReady + "/" + numModules);
                    Messenger<int,int>.Broadcast(StartupEvent.ManagersProgress, numReady, numModules);
                }
                
                yield return null;
            }
            
            Debug.Log("All managers started up");
            Messenger.Broadcast(StartupEvent.ManagersStarded);
        }
    }
}