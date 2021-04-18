using UnityEngine;

namespace Ch2.Scripts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefub;
        private GameObject _enemy;

        private void Update()
        {
            if (_enemy != null) return;
            _enemy = Instantiate(enemyPrefub);
            _enemy.transform.position = new Vector3(0f, 1f, 0f);
            var angle = Random.Range(0f, 360f);
            _enemy.transform.Rotate(0f, angle, 0f);
        }
    }
}