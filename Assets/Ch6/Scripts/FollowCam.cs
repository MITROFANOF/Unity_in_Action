using UnityEngine;

namespace Ch6.Scripts
{
    public class FollowCam : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.2f;
        
        private Vector3 _velocity = Vector3.zero;
        
        private void LateUpdate()
        {
            var camTransform = transform;
            var camCurrentPosition = camTransform.position;
            var targetPosition = target.position;
            var camEndPosition = new Vector3(targetPosition.x, targetPosition.y, camCurrentPosition.z);
            
            camCurrentPosition = Vector3.SmoothDamp(camCurrentPosition, camEndPosition, ref _velocity, smoothTime);
            camTransform.position = camCurrentPosition;
        }
    }
}
