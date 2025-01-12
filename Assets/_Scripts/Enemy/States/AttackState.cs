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
        private StepSoundController _soundController;
        
        private IDamageable _damageable;

        private float Pitch =>
            _scriptableObject.defaultStepsSoundPitch * (_scriptableObject.chaseSpeed / _scriptableObject.patrolSpeed);
        
        public AttackState(NavMeshAgent agent, Transform target, 
            EnemyScriptableObject scriptableObject, StepSoundController soundController)
        {
            _target = target;
            _agent = agent;
            _scriptableObject = scriptableObject;
            _soundController = soundController;

            _agent.speed = _scriptableObject.chaseSpeed;
            _damageable = target.GetComponent<IDamageable>() ?? target.GetComponentInParent<IDamageable>();
        }

        public void Stay()
        {
            _agent.SetDestination(_target.position);
            _soundController.SoundOn(Pitch);
            
            _damageable.TakeDamage(CalculateDamage() * Time.deltaTime);
        }

        private float CalculateDamage()
        {
            float distance = Vector2.Distance(_agent.transform.position, _target.position);
            float kof = 1 - (distance - _agent.stoppingDistance) / (_scriptableObject.attackDistance - _agent.stoppingDistance);
            return Mathf.Lerp(_scriptableObject.minDamage, _scriptableObject.maxDamage, kof);
        }

        public void Exit()
        {
            _agent.ResetPath();
            _soundController.SoundOff();
        }

        public void Enter() { }
        public void OnDestroy() { }
    }
}