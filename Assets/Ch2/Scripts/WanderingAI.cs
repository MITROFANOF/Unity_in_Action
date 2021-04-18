using System.Collections;
using UnityEngine;

namespace Ch2.Scripts
{
    public class WanderingAI : MonoBehaviour
    {
        public float speed = 3.0f;
        public float obstacleRange = 5.0f;
        
        [SerializeField] private float castRadius = 0.75f;
        [SerializeField] private float turnAngle = 110f;

        private void Update()
        {
            var enemyTransform = transform;
            

            var ray = new Ray(enemyTransform.position, enemyTransform.forward);
            if (!Physics.SphereCast(ray, castRadius, out var hit)) return;
            if (!(hit.distance < obstacleRange))
            {
                enemyTransform.Translate(0f, 0f, speed * Time.deltaTime);
            }
            else
            {
                var angle = Random.Range(-turnAngle, turnAngle); 
                enemyTransform.Rotate(0f, angle, 0f);
            }
        }
    }
}
