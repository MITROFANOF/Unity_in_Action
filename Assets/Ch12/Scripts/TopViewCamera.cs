using UnityEngine;

namespace Ch12.Scripts
{
    public class TopViewCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;

        public float cameraDistance = 15f;
        public float cameraLerpSpeed = 3f;
        
        private Vector3 _cameraOffset;
        private const float SphereCastRadius = 0.5f;

        private void Start()
        {
            _cameraOffset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            var hitDetected = Physics.SphereCast(target.position, SphereCastRadius, _cameraOffset.normalized, out var hit, cameraDistance);

            Vector3 cameraTargetPos;
            
            if (hitDetected)
                cameraTargetPos = target.position + _cameraOffset.normalized * (hit.distance + SphereCastRadius);
            else
                cameraTargetPos = target.position + _cameraOffset;
            
            transform.position = Vector3.Lerp(transform.position, cameraTargetPos, cameraLerpSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}