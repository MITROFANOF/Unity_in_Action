using System.Collections.Generic;
using System.Linq;
using Ch10.Scripts;
using UnityEngine;

namespace Ch9.Scripts
{
    public class InventoryManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public string EquippedItem { get; private set; }

        private Dictionary<string, int> _items;


        public void Startup()
        {
            Debug.Log("Inventory manager starting...");

            _items = new Dictionary<string, int>();

            Status = ManagerStatus.Started;
        }

        public bool EquipItem(string itemName)
        {
            if (_items.ContainsKey(itemName) && EquippedItem != itemName)
            {
                EquippedItem = itemName;
                return true;
            }

            EquippedItem = null;
            return false;
        }

        public bool ConsumeItem(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName]--;
                if (_items[itemName] == 0)
                {
                    _items.Remove(itemName);
                }
            }
            else
            {
                return false;
            }

            DisplayItems();
            return true;
        }

        private void DisplayItems()
        {
            var itemDisplay = _items.Aggregate("Items: ", (current, item) => current + $"{item.Key} ({item.Value}) ");
            Debug.Log(itemDisplay);
        }

        public void AddItem(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName]++;
            }
            else
            {
                _items[itemName] = 1;
            }

            DisplayItems();
        }

        public List<string> GetItemList()
        {
            return new List<string>(_items.Keys);
        }

        public int GetItemCount(string itemName)
        {
            return _items.ContainsKey(itemName) ? _items[itemName] : 0;
        }
    }
}