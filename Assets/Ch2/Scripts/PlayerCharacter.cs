using UnityEngine;

namespace Ch2.Scripts
{
    public class PlayerCharacter : MonoBehaviour
    {
        private int _health;

        private void Start()
        {
            _health = 5;
        }

        public void Hurt(int damage)
        {
            _health -= damage;
            Debug.Log("Health: " + _health);
        }
    }
}