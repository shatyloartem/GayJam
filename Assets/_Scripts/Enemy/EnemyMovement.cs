using System;
using System.Collections;
using _Scripts.Enemy.States;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyScriptableObject enemyScriptableObject;
        [SerializeField] private LayerMask obstacleLayer;
        
        [Space(7)]
        
        [SerializeField] private Transform[] patrolPoints;

        private StateMachine _stateMachine;
        private IEnumerator _cooldownCoroutine;
        
        private NavMeshAgent _agent;
        private Detector _detector;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = enemyScriptableObject.speed;
            _agent.stoppingDistance = enemyScriptableObject.distanceToStop;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            
            _detector = GetComponentInChildren<Detector>();
            _detector.SetDistance(enemyScriptableObject.detectDistance);
            _detector.OnPlayerEntered += OnPlayerDetected;
            _detector.OnPlayerExited += OnPlayerExit;
            
            _stateMachine = new StateMachine(new PatrolState(this, enemyScriptableObject, _agent, patrolPoints));
        }

        private void Update() => _stateMachine.UpdateState();
        private void OnDestroy() => _stateMachine.OnDestroy();

        private void OnPlayerDetected()
        {
            _cooldownCoroutine = AttackCooldownCoroutine();
            StartCoroutine(_cooldownCoroutine);
        }

        private void OnPlayerExit()
        {
            StopCoroutine(_cooldownCoroutine);
            _stateMachine.ChangeState(new PatrolState(this, enemyScriptableObject, _agent, patrolPoints));
        }

        private IEnumerator AttackCooldownCoroutine()
        {
            yield return new WaitForSeconds(enemyScriptableObject.attackCooldown);
            if (!_detector.IsPlayerInRange) yield break;
            
            Vector2 direction = _detector.Player.transform.position - transform.position;
            var hit = Physics2D.Raycast(transform.position, direction.normalized, 
                direction.magnitude, obstacleLayer);
            
            if (hit.collider)
            {
                _cooldownCoroutine = AttackCooldownCoroutine();
                StartCoroutine(_cooldownCoroutine);
                yield break;
            }
            
            Attack();
        }
        
        private void Attack()
        {
            _stateMachine.ChangeState(new AttackState(_agent, _detector.Player.transform));
        }
    }
}
