using UnityEngine;

namespace Ch8.Scripts
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class RelativeMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public float rotSpeed = 15f;
        public float moveSpeed = 6f;
        public float baseSpeed = 6f;
        public float jumpSpeed = 15f;
        public float gravity = -9.8f;
        public float terminalVelocity = -10f;
        public float minFall = -1.5f;
        public float pushForce = 3f;

        private float _vertSpeed;
        private ControllerColliderHit _contact;
        private CharacterController _characterController;
        private Animator _animator;
        private static readonly int SpeedParam = Animator.StringToHash("speed");
        private static readonly int JumpingParam = Animator.StringToHash("jumping");

        
        
        private void Start()
        {
            _vertSpeed = minFall;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
        }

        private void OnDestroy()
        {
            Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
        }

        private void OnSpeedChanged(float value)
        {
            moveSpeed = baseSpeed * value;
            _animator.speed = value;
        }
        
        private void Update()
        {
            var movement = Vector3.zero;

            var horInput = Input.GetAxis("Horizontal");
            var vertInput = Input.GetAxis("Vertical");
            if (horInput != 0 || vertInput != 0)
            {
                movement.x = horInput * moveSpeed;
                movement.z = vertInput * moveSpeed;
                movement = Vector3.ClampMagnitude(movement, moveSpeed);

                var tmp = target.rotation;
                target.eulerAngles = new Vector3(0f, target.eulerAngles.y, 0f);
                movement = target.TransformDirection(movement);
                target.rotation = tmp;

                var direction = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            }


            var hitGround = false;
            if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out var hit))
            {
                var check = (_characterController.height + _characterController.radius) / 1.9f;
                hitGround = hit.distance <= check;
            }

            _animator.SetFloat(SpeedParam, movement.sqrMagnitude);
            
            if (hitGround)
            {
                if (Input.GetButtonDown("Jump"))
                    _vertSpeed = jumpSpeed;
                else
                {
                    _vertSpeed = minFall;
                    _animator.SetBool(JumpingParam, false);
                }
            }
            else
            {
                _vertSpeed += gravity * 5 * Time.deltaTime;
                if (_vertSpeed < terminalVelocity)
                {
                    _vertSpeed = terminalVelocity;
                }

                if (_contact != null)
                {
                    _animator.SetBool(JumpingParam, true);
                    if (_characterController.isGrounded)
                    {
                        if (Vector3.Dot(movement, _contact.normal) < 0)
                        {
                            movement = _contact.normal * moveSpeed;
                        }
                        else
                        {
                            movement += _contact.normal * moveSpeed;
                        }
                    }
                }

                
            }

            movement.y = _vertSpeed;

            _characterController.Move(movement * Time.deltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _contact = hit;

            var body = hit.collider.attachedRigidbody;
            if (body != null && !body.isKinematic)
            {
                body.velocity = hit.moveDirection * pushForce;
            }
        }
    }
}