using System;
using UnityEngine;

namespace Ch2.Scripts
{
    public class MouseLook : MonoBehaviour
    {
        public enum RotationAxes
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }

        public RotationAxes axes = RotationAxes.MouseXAndY;

        public float sensitivityHor = 9.0f;
        public float sensitivityVert = 9.0f;

        public float minVert = -45.0f;
        public float maxVert = 45.0f;

        private float _rotationX;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            var body = GetComponent<Rigidbody>();
            if (body != null)
                body.freezeRotation = true;
        }

        private void Update()
        {
            switch (axes)
            {
                case RotationAxes.MouseX:
                    RotateHor();
                    break;
                case RotationAxes.MouseY:
                    RotateVert();
                    break;
                case RotationAxes.MouseXAndY:
                    RotateHor();
                    RotateVert();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void RotateHor()
        {
            _transform.Rotate(0f, Input.GetAxis("Mouse X") * sensitivityHor, 0f);
        }

        private void RotateVert()
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            var rotationY = _transform.localEulerAngles.y;

            _transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}