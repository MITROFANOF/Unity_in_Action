using System;
using UnityEngine;

namespace Ch9.Scripts
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private string itemName;

        private void OnTriggerEnter(Collider other)
        {
            Managers.Inventory.AddItem(itemName);
            Destroy(gameObject);
        }
    }
}
