using Ch7.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ch2.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class WanderingAI : MonoBehaviour
    {
        [SerializeField] private GameObject fireballPrefab;
        [SerializeField] private AudioSource fireSound;
        
        public float shootRange = 10.0f;
        public float wallDetectRange = 1.0f;
        public float turnAngle = 90f;

        
        private GameObject _firaball;
        private CharacterController _controller;
        
        private bool _alive;
        private static float _speed = 3.0f;
        private const float BaseSpeed = 3.0f;
        private const float Gravity = -9.8f;
        private const float CastRadius = 0.75f;

        private void Awake()
        {
            Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChanged);
        }

        private void OnDestroy()
        {
            Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChanged);
        }

        private static void OnSpeedChanged(float value)
        {
            _speed = BaseSpeed * value;
        }

        private void Start()
        {
            _alive = true;
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!_alive) return;

            var enemyTransform = transform;
            var ray = new Ray(enemyTransform.position, enemyTransform.forward);

            if (Physics.SphereCast(ray, CastRadius, out var targetHit, shootRange))
            {
                
                if (targetHit.transform.gameObject.GetComponent<PlayerCharacter>())
                {
                    if (_firaball) return;
                    _firaball = Instantiate(fireballPrefab);
                    fireSound.PlayOneShot(fireSound.clip);
                    _firaball.transform.position = enemyTransform.TransformPoint(Vector3.forward * 1.5f);
                    _firaball.transform.rotation = enemyTransform.rotation;
                }
                
            }
            if(Physics.SphereCast(ray, CastRadius, out _, wallDetectRange))
            {
                var angle = Random.Range(-turnAngle, turnAngle);
                enemyTransform.Rotate(0f, angle, 0f);
            }
            else
            {
                var movement = new Vector3(0f, Gravity, _speed);
                movement *= Time.deltaTime;
                movement = transform.TransformDirection(movement);
                _controller.Move(movement);
            }
        }

        public void SetAlive(bool alive)
        {
            _alive = alive;
        }
        
        public bool IsAlive()
        {
            return _alive;
        }
    }
}