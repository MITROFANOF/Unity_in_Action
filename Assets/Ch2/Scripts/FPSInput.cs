using UnityEngine;

namespace Ch2.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control Script/FPS Input")]
    public class FPSInput : MonoBehaviour
    {
        public float speed = 6.0f;
        public float gravity = -9.8f;
        
        private CharacterController _controller;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var deltaX = Input.GetAxis("Horizontal") * speed;
            var deltaZ = Input.GetAxis("Vertical") * speed;

            var movement = new Vector3(deltaX, 0f, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _controller.Move(movement);
        }
    }
}