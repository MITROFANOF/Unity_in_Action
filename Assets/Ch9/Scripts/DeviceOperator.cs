using UnityEngine;

namespace Ch9.Scripts
{
    public class DeviceOperator : MonoBehaviour
    {
        public float radius = 1.5f;

        private void Update()
        {
            if (!Input.GetButtonDown("Fire3")) return;
            
            var hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                var direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.5f) 
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
