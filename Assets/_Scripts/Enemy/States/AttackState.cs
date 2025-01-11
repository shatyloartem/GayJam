using _Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemy.States
{
    public class AttackState : IState
    {
        private Transform _target;
        private NavMeshAgent _agent;
        
        public AttackState(NavMeshAgent agent, Transform target)
        {
            _target = target;
            _agent = agent;
        }
        
        public void Enter()
        {
            
        }

        public void Stay()
        {
            _agent.SetDestination(_target.position);
        }

        public void Exit()
        {
            
        }

        public void OnDestroy() { }
    }
}