using UnityEngine;

namespace Ch2.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefub;
        private GameObject _enemy;

        private void Update()
        {
            if (_enemy) return;
            _enemy = Instantiate(enemyPrefub, transform);
            var angle = Random.Range(0f, 360f);
            _enemy.transform.Rotate(0f, angle, 0f);
        }
    }
}