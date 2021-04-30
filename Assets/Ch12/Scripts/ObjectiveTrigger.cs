using UnityEngine;

namespace Ch12.Scripts
{
    public class ObjectiveTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
                MissionManager.ReachObjective();
        }
    }
}
