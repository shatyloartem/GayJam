using UnityEngine;

namespace _Scripts.Enemy
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Enemy", fileName = "Enemy SO")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public float speed = 3.5f;
        public float waitOnPointTime = 3.5f;
        public float distanceToStop = .5f;
        public float detectDistance = 4f;
        public float attackCooldown = 2;
        public float attackDistance = 6.5f;
    }
}