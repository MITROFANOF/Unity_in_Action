using UnityEngine;

namespace Ch9.Scripts
{
    public class BasicUI : MonoBehaviour
    {
        private void OnGUI()
        {
            var posX = 10;
            var posY = 10;
            const int width = 100;
            const int height = 30;
            const int buffer = 10;

            var itemList = Managers.Inventory.GetItemList();
            if (itemList.Count == 0)
            {
                GUI.Box(new Rect(posX, posY, width, height), "No Items");
            }

            foreach (var item in itemList)
            {
                var count = Managers.Inventory.GetItemCount(item);
                var image = Resources.Load<Texture2D>("Icons/" + item);
                GUI.Box(new Rect(posX, posY, width, height), new GUIContent($"({count})", image));
                posX += width + buffer;
            }

            var equipped = Managers.Inventory.EquippedItem;
            if (equipped != null)
            {
                posX = Screen.width - (width + buffer);
                var image = Resources.Load<Texture2D>("Icons/" + equipped);
                GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
            }

            posX = 10;
            posY += height + buffer;

            foreach (var item in itemList)
            {
                if (item == "health")
                {
                    if (GUI.Button(new Rect(posX, posY, width, height), "Use Health"))
                    {
                        Managers.Inventory.ConsumeItem("health");
                        Managers.Player.ChangeHealth(25);
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(posX, posY, width, height), "Equip " + item))
                    {
                        Managers.Inventory.EquipItem(item);
                    }

                }

                posX += width + buffer;
            }
        }
    }
}
