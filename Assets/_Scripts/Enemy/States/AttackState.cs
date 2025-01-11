using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemy.States
{
    public class AttackState : IState
    {
        private Transform _target;
        private NavMeshAgent _agent;
        private EnemyScriptableObject _scriptableObject;
        
        private IDamageable _damageable;
        
        public AttackState(NavMeshAgent agent, Transform target, EnemyScriptableObject scriptableObject)
        {
            _target = target;
            _agent = agent;
            _scriptableObject = scriptableObject;
            
            _damageable = target.GetComponent<IDamageable>();
        }

        public void Stay()
        {
            _agent.SetDestination(_target.position);
            _damageable.TakeDamage(CalculateDamage() * Time.deltaTime);
        }

        private float CalculateDamage()
        {
            float distance = Vector2.Distance(_agent.transform.position, _target.position);
            float kof = 1 - (distance - _agent.stoppingDistance) / (_scriptableObject.attackDistance - _agent.stoppingDistance);
            return Mathf.Lerp(_scriptableObject.minDamage, _scriptableObject.maxDamage, kof);
        }
        
        public void Enter() { }
        public void Exit() { }
        public void OnDestroy() { }
    }
}