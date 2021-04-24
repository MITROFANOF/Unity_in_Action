using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ch2.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class RayShooter : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            _camera = GetComponent<Camera>();
        }

        private void OnGUI()
        {
            const int size = 12;
            var posX = _camera.pixelWidth / 2f - size / 4f;
            var posY = _camera.pixelHeight / 2f - size / 2f;
            GUI.Label(new Rect(posX, posY, size, size), "*");
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
            var point = new Vector2(_camera.pixelWidth / 2f, _camera.pixelHeight / 2f);
            var ray = _camera.ScreenPointToRay(point);
            if (!Physics.Raycast(ray, out var hit)) return;
            var hitObject = hit.transform.gameObject;
            var target = hitObject.GetComponent<ReactiveTarget>();
            if (target)
            {
                target.ReactToHit();
                Messenger.Broadcast(GameEvent.ENEMY_HIT);
            }
            else
            {
                StartCoroutine(SphereIndicator(hit.point));
            }
        }

        private static IEnumerator SphereIndicator(Vector3 hitInfoPoint)
        {
            var sphere = GameObject.CreatePrimitive((PrimitiveType.Sphere));
            sphere.transform.position = hitInfoPoint;

            yield return new WaitForSeconds(1f);

            Destroy(sphere);
        }
    }
}