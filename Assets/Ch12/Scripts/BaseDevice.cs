using UnityEngine;

namespace Ch12.Scripts
{
    public class BaseDevice : MonoBehaviour
    {
        public float radius = 3.5f;

        private void OnMouseDown()
        {
            var player = GameObject.FindWithTag("Player").transform;
            if (!(Vector3.Distance(player.position, transform.position) < radius)) return;
            var direction = transform.position - player.position;
            if (Vector3.Dot(player.forward, direction) > 0.5f)
            {
                Operate();
            }
        }

        protected virtual void Operate()
        {
            
        }
    }
}
