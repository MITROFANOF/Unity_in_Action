using System;
using UnityEngine;

namespace Ch9.Scripts
{
    public class DeviceTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject[] targets;

        public bool requireKey;

        private void OnTriggerEnter(Collider other)
        {
            if (requireKey && Managers.Inventory.EquippedItem != "key") return;
            foreach (var target in targets)
            {
                target.SendMessage("Activate");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            foreach (var target in targets)
            {
                target.SendMessage("Deactivate");
            }
        }
    }
}
