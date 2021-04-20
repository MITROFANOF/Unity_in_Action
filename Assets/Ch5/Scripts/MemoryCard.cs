using UnityEngine;

namespace Ch5.Scripts
{
    public class MemoryCard : MonoBehaviour
    {
        [SerializeField] private GameObject cardBack;
        [SerializeField] private SceneController controller;

        public int ID { get; private set; }

        public void SetCard(int cardID, Sprite image)
        {
            ID = cardID;
            GetComponent<SpriteRenderer>().sprite = image;
        }
        
        private void OnMouseDown()
        {
            if (!cardBack.activeSelf || !controller.CanReveal) return;
            
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }

        public void Unreveal()
        {
            cardBack.SetActive(true);
        }
    }
}
