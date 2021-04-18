using UnityEngine;

namespace Ch2.Scripts
{
    public class Fireball : MonoBehaviour
    {
        public float speed = 10f;
        public int damage = 1;

        private void Update()
        {
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                player.Hurt(damage);
            }

            Destroy(gameObject);
        }
    }
}