using Ch9.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ch12.Scripts
{
    public class InventoryPopup : MonoBehaviour
    {
        [SerializeField] private Image[] itemIcons;
        [SerializeField] private Text[] itemLabels;
        [SerializeField] private Text currentItemLabel;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button useButton;

        private string _currentItem;        
        
        public void Refresh()
        {
            var itemList = Managers.Inventory.GetItemList();
            var lenght = itemIcons.Length;
            for (var i = 0; i < lenght; i++)
            {
                if (i < itemList.Count)
                {
                    itemIcons[i].gameObject.SetActive(true);
                    itemLabels[i].gameObject.SetActive(true);

                    var item = itemList[i];

                    var sprite = Resources.Load<Sprite>("Icons/" + item);
                    itemIcons[i].sprite = sprite;
                    itemIcons[i].SetNativeSize();

                    var itemCount = Managers.Inventory.GetItemCount(item);
                    var itemLabel = "x" + itemCount;
                    if (item == Managers.Inventory.EquippedItem)
                    {
                        itemLabel = "Equipped\n" + itemLabel;
                    }
                    itemLabels[i].text = itemLabel;

                    var entry = new EventTrigger.Entry {eventID = EventTriggerType.PointerClick};
                    entry.callback.AddListener(_ => { OnItem(item); });

                    var trigger = itemIcons[i].GetComponent<EventTrigger>();
                    trigger.triggers.Clear();
                    trigger.triggers.Add(entry);
                }
                else
                {
                    itemIcons[i].gameObject.SetActive(false);
                    itemLabels[i].gameObject.SetActive(false);
                }
            }

            if (!itemList.Contains(_currentItem))
            {
                _currentItem = null;
            }

            if (_currentItem == null)
            {
                currentItemLabel.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false);
                useButton.gameObject.SetActive(false);
            }
            else
            {
                currentItemLabel.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(true);
                useButton.gameObject.SetActive(_currentItem == "health");
                currentItemLabel.text = _currentItem;
            }
        }

        private void OnItem(string item)
        {
            _currentItem = item;
            Refresh();
        }

        public void OnEquip()
        {
            Managers.Inventory.EquipItem(_currentItem);
            Refresh();
        }

        public void OnUse()
        {
            Managers.Inventory.ConsumeItem(_currentItem);
            if(_currentItem == "health")
                Managers.Player.ChangeHealth(25);
            Refresh();
        }
    }
}
