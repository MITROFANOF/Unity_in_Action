using UnityEngine;

namespace Ch8.Scripts
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        public float rotSpeed = 1.5f;
        private float _rotY;
        private Vector3 _offset;
        private Transform _cameraTransform;
        
        private void Start()
        {
            _cameraTransform = transform;
            _rotY = _cameraTransform.eulerAngles.y;
            _offset = target.position - _cameraTransform.position;
         }

        private void LateUpdate()
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            var rotation  = Quaternion.Euler(0f, _rotY, 0f);
            _cameraTransform.position = target.position - (rotation * _offset);
            _cameraTransform.LookAt(target);
        }
    }
}
