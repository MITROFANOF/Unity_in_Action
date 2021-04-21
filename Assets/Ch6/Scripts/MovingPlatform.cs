using System;
using UnityEngine;

namespace Ch6.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 finishPos = Vector3.zero;
        public float speed = 0.5f;

        private Vector3 _startPos;
        private float _trackPercent;
        private int _direction = 1;

        private void Start()
        {
            _startPos = transform.position;
        }

        private void Update()
        {
            _trackPercent += _direction * speed * Time.deltaTime;
            var x = (finishPos.x - _startPos.x) * _trackPercent + _startPos.x;
            var y = (finishPos.y - _startPos.y) * _trackPercent + _startPos.y;
            transform.position = new Vector3(x, y, _startPos.z);

            if ((_direction == 1 && _trackPercent > 0.9f) || (_direction == -1 && _trackPercent < 0.1f))
            {
                _direction *= -1;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, finishPos);
        }
    }
}
