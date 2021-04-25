using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ch9.Scripts
{
    [RequireComponent(typeof(PlayerManager), typeof(InventoryManager))]
    public class Managers : MonoBehaviour
    {
        public static PlayerManager Player { get; private set; }
        public static InventoryManager Inventory { get; private set; }

        private List<IGameManager> _startSequence;

        private void Awake()
        {
            Player = GetComponent<PlayerManager>();
            Inventory = GetComponent<InventoryManager>();

            _startSequence = new List<IGameManager> {Player, Inventory};

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
                    Debug.Log("Progress: " + numReady + "/" + numModules);;
                yield return null;
            }
            
            Debug.Log("All managers started up");
        }
    }
}