using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using IState = _Scripts.Interfaces.IState;

namespace _Scripts.Enemy.States
{
    public class PatrolState : IState
    {
        // TODO: Make in Scriptable Object
        private const int _waitingTime = 3;
        
        private EnemyMovement _owner;
        private NavMeshAgent _agent;

        private Transform[] _patrolPoints;
        private int _currentPoint;
        private bool _isWaiting;
        
        public PatrolState(EnemyMovement owner, NavMeshAgent agent, Transform[] patrolPoints)
        {
            _owner = owner;
            _agent = agent;
            _patrolPoints = patrolPoints;
        }
        
        public void Enter()
        {
            _currentPoint = ClosestPoint();
            _agent.SetDestination(_patrolPoints[_currentPoint].position);
        }

        public void Stay()
        {
            if(!_isWaiting && _agent.remainingDistance <= _agent.stoppingDistance)
                WaitOnPoint();
        }

        public void Exit()
        {
            _agent.ResetPath();
        }

        private async void WaitOnPoint()
        {
            _isWaiting = true;
            _agent.ResetPath();
            
            await Task.Delay(_waitingTime * 1000);
            
            _isWaiting = false;

            _currentPoint++;
            if(_currentPoint >= _patrolPoints.Length)
                _currentPoint = 0;
            
            _agent?.SetDestination(_patrolPoints[_currentPoint].position);
        }
        
        private int ClosestPoint()
        {
            int closestPoint = 0;
            float shortestDistance = float.MaxValue;

            for (int i = 0; i < _patrolPoints.Length; i++)
            {
                if(Vector2.Distance(_owner.transform.position, _patrolPoints[i].position) > shortestDistance)
                    continue;
                
                closestPoint = i;
                shortestDistance = Vector2.Distance(_owner.transform.position, _patrolPoints[i].position);
            }
            
            return closestPoint;
        }
    }
}