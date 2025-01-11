using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using IState = _Scripts.Interfaces.IState;

namespace _Scripts.Enemy.States
{
    public class PatrolState : IState
    {
        private EnemyScriptableObject _scriptableObject;
        private EnemyMovement _owner;
        private NavMeshAgent _agent;

        private Transform[] _patrolPoints;

        private IEnumerator _waitCoroutine;
        private int _currentPoint;
        private bool _isWaiting;
        
        public PatrolState(EnemyMovement owner, EnemyScriptableObject scriptableObject, 
            NavMeshAgent agent, Transform[] patrolPoints)
        {
            _owner = owner;
            _agent = agent;
            _patrolPoints = patrolPoints;
            _scriptableObject = scriptableObject;
        }
        
        public void Enter()
        {
            _currentPoint = ClosestPoint();
            _agent.SetDestination(_patrolPoints[_currentPoint].position);
        }

        public void Stay()
        {
            if (_isWaiting || !(_agent.remainingDistance <= _agent.stoppingDistance)) return;
            
            _waitCoroutine = WaitOnPoint();
            _owner.StartCoroutine(_waitCoroutine);
        }

        public void Exit()
        {
            if(_waitCoroutine != null)
                _owner.StopCoroutine(_waitCoroutine);

            _agent.ResetPath();
        }

        public void OnDestroy()
        {
            if(_waitCoroutine != null)
                _owner.StopCoroutine(_waitCoroutine);
        }

        private IEnumerator WaitOnPoint()
        {
            _isWaiting = true;
            _agent.ResetPath();
            
            yield return new WaitForSeconds(_scriptableObject.waitOnPointTime);
            
            _isWaiting = false;

            _currentPoint++;
            if(_currentPoint >= _patrolPoints.Length)
                _currentPoint = 0;

            if(Application.isPlaying && _agent.isActiveAndEnabled)
                _agent.SetDestination(_patrolPoints[_currentPoint].position);
            
            _waitCoroutine = null;
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