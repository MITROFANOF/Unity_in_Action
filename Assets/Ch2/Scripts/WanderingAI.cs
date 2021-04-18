using UnityEngine;
using Random = UnityEngine.Random;

namespace Ch2.Scripts
{
    public class WanderingAI : MonoBehaviour
    {
        public float speed = 3.0f;
        public float obstacleRange = 5.0f;

        [SerializeField] private GameObject fireballPrefab;
        private GameObject _firaball;

        private bool _alive;
        private const float CastRadius = 0.75f;
        private const float TurnAngle = 110f;

        private void Start()
        {
            _alive = true;
        }

        private void Update()
        {
            if (!_alive) return;

            var enemyTransform = transform;
            var ray = new Ray(enemyTransform.position, enemyTransform.forward);

            if (Physics.SphereCast(ray, CastRadius, out var hit, obstacleRange))
            {
                if (hit.transform.gameObject.GetComponent<PlayerCharacter>())
                {
                    if (_firaball != null) return;
                    _firaball = Instantiate(fireballPrefab);
                    _firaball.transform.position = enemyTransform.TransformPoint(Vector3.forward * 1.5f);
                    _firaball.transform.rotation = enemyTransform.rotation;
                }
                else
                {
                    var angle = Random.Range(-TurnAngle, TurnAngle);
                    enemyTransform.Rotate(0f, angle, 0f);
                }
            }
            else
            {
                enemyTransform.Translate(0f, 0f, speed * Time.deltaTime);
            }
        }

        public void SetAlive(bool alive)
        {
            _alive = alive;
        }
    }
}