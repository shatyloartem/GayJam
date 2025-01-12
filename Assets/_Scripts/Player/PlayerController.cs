using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static Collider2D Collider { get; private set; }
        
        private void Awake() => Collider = GetComponent<Collider2D>();
    }
}
