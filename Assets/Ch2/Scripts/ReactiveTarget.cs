using System.Collections;
using UnityEngine;

namespace Ch2.Scripts
{
    public class ReactiveTarget : MonoBehaviour
    {
        public void ReactToHit()
        {
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            transform.Rotate(-75f, 0f, 0f);

            yield return new WaitForSeconds(1.5f);
            
            Destroy(gameObject);
        }
    }
}
