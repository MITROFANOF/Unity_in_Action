using Ch7.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ch12.Scripts
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PointClickMovement : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        
        public float rotSpeed = 15f;
        public float moveSpeed = 6f;
        public float baseSpeed = 6f;
        public float gravity = -9.8f;
        public float terminalVelocity = -10f;
        public float minFall = -1.5f;
        public float pushForce = 3f;
        public float deceleration = 25f;
        public float targetBuffer = 1.5f;

        private float _curSpeed;
        private Vector3 _targetPos = Vector3.one;
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
            Messenger<float>.AddListener(GameEvent.SpeedChanged, OnSpeedChanged);
        }

        private void OnDestroy()
        {
            Messenger<float>.RemoveListener(GameEvent.SpeedChanged, OnSpeedChanged);
        }

        private void OnSpeedChanged(float value)
        {
            moveSpeed = baseSpeed * value;
            _animator.speed = value;
        }

        private void Update()
        {
            var movement = Vector3.zero;

            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out var mouseHit))
                {
                    var hitObject = mouseHit.transform.gameObject;
                    if(hitObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        _targetPos = mouseHit.point;
                        _curSpeed = moveSpeed;
                    }
                }
            }

            if (_targetPos != Vector3.one)
            {
                if (_curSpeed > moveSpeed * 0.5f)
                {
                    var position = transform.position;
                    var adjustedPos = new Vector3(_targetPos.x, position.y, _targetPos.z);
                    var targetRot = Quaternion.LookRotation(adjustedPos - position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
                }

                movement = _curSpeed * Vector3.forward;
                movement = transform.TransformDirection(movement);

                if (Vector3.Distance(_targetPos, transform.position) < targetBuffer)
                {
                    _curSpeed -= deceleration * Time.deltaTime;
                    if (_curSpeed <= 0)
                    {
                        _targetPos = Vector3.one;
                    }
                }
            }

            var hitGround = false;
            if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out var hit))
            {
                var check = (_characterController.height + _characterController.radius) / 1.9f;
                hitGround = hit.distance <= check;
            }

            if (hitGround)
            {
                _vertSpeed = minFall;
                _animator.SetBool(JumpingParam, false);
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

            _animator.SetFloat(SpeedParam, movement.sqrMagnitude);

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