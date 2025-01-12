using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Utilities
{
    public class PlayerLayers : MonoBehaviour
    {
        private SpriteRenderer[] _spriteRenderers;
        private Collider2D _boxCollider;
        
        private void Awake()
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _boxCollider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            SetLayer(PlayerController.Collider.bounds.center.y > _boxCollider.bounds.max.y ? "TopLayer" : "Default");
        }

        private void SetLayer(string layer)
        {
            foreach (var spriteRenderer in _spriteRenderers)
                spriteRenderer.sortingLayerName = layer;
        }
    }
}
