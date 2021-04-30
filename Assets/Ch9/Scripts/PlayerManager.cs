using Ch7.Scripts;
using UnityEngine;

namespace Ch9.Scripts
{
    public class PlayerManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus Status { get; private set; }

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public void Startup()
        {
            Debug.Log("Player manager starting...");

            UpdateData(50, 100);

            Status = ManagerStatus.Started;
        }

        public void UpdateData(int health, int maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }

        public void ChangeHealth(int value)
        {
            Health += value;
            if (Health > MaxHealth)
                Health = MaxHealth;
            else if (Health <= 0)
            {
                Health = 0;
                Messenger.Broadcast(GameEvent.LevelFailed);
            }

            Messenger.Broadcast(GameEvent.HealthChanged);
        }

        public void Respawn()
        {
            UpdateData(50, 100);
        }
    }
}