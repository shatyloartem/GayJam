using UnityEngine;

namespace _Scripts.Utilities
{
    public class Checkpoint : MonoBehaviour
    {
        private static Vector2 _savedPosition = Vector2.zero;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                Save(other.transform.position);
        }

        public static Vector2 Load() => _savedPosition;
        public static void ResetCheckpoint() => _savedPosition = Vector2.zero;
        private static void Save(Vector2 positionToSave) => _savedPosition = positionToSave;
    }
}
