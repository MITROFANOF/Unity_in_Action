using Ch7.Scripts;
using UnityEngine;

namespace Ch2.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control Script/FPS Input")]
    public class FPSInput : MonoBehaviour
    {
        private float _speed = 6.0f;
        private const float BaseSpeed = 6.0f;
        public float gravity = -9.8f;

        private CharacterController _controller;

        private void Awake()
        {
            Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChanged);
        }

        private void OnDestroy()
        {
            Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChanged);
        }

        private void OnSpeedChanged(float value)
        {
            _speed = BaseSpeed * value;
        }
        
        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var deltaX = Input.GetAxis("Horizontal") * _speed;
            var deltaZ = Input.GetAxis("Vertical") * _speed;

            var movement = new Vector3(deltaX, 0f, deltaZ);
            movement = Vector3.ClampMagnitude(movement, _speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _controller.Move(movement);
        }
    }
}