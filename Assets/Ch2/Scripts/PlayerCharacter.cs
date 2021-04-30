using Ch9.Scripts;
using UnityEngine;

namespace Ch2.Scripts
{
    public class PlayerCharacter : MonoBehaviour
    {
        public static void Hurt(int damage)
        {
            Managers.Player.ChangeHealth(-damage);
        }
    }
}