using System;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class Detector : MonoBehaviour
    {
        public event Action OnPlayerEntered;
        public event Action OnPlayerExited;
        
        public GameObject Player { get; private set; }
        public bool IsPlayerInRange { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player"))
                return;
            
            Player = other.gameObject;
            IsPlayerInRange = true;
            OnPlayerEntered?.Invoke();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.CompareTag("Player"))
                return;
            
            Player = null;
            IsPlayerInRange = false;
            OnPlayerExited?.Invoke();
        }

        public void SetDistance(float distance) => GetComponent<CircleCollider2D>().radius = distance;
    }
}