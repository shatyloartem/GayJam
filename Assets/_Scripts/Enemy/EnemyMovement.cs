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
        
        [SerializeField] private Detector searchDetector;
        [SerializeField] private Detector attackDetector;

        [Space(7)]
        
        [SerializeField] private Transform[] patrolPoints;

        private StateMachine _stateMachine;
        private IEnumerator _cooldownCoroutine;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = enemyScriptableObject.speed;
            _agent.stoppingDistance = enemyScriptableObject.distanceToStop;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            
            attackDetector.SetDistance(enemyScriptableObject.attackDistance);
            attackDetector.OnPlayerExited += OnPlayerAttackExit;
            
            searchDetector.SetDistance(enemyScriptableObject.detectDistance);
            searchDetector.OnPlayerEntered += OnPlayerSearchEnter;
            searchDetector.OnPlayerExited += OnPlayerSearchExit;
            
            _stateMachine = new StateMachine(new PatrolState(this, enemyScriptableObject, _agent, patrolPoints));
        }

        private void Update() => _stateMachine.UpdateState();
        
        private void OnDestroy()
        {
            attackDetector.OnPlayerExited -= OnPlayerAttackExit;
            searchDetector.OnPlayerEntered -= OnPlayerSearchEnter;
            searchDetector.OnPlayerExited -= OnPlayerSearchExit;
            
            _stateMachine.OnDestroy();
        }

        private void OnPlayerSearchEnter()
        {
            _cooldownCoroutine = AttackCooldownCoroutine();
            StartCoroutine(_cooldownCoroutine);
        }

        private void OnPlayerSearchExit() => StopCoroutine(_cooldownCoroutine);

        private void OnPlayerAttackExit()
        {
            _stateMachine.ChangeState(new PatrolState(this, enemyScriptableObject, _agent, patrolPoints));
        }

        private IEnumerator AttackCooldownCoroutine()
        {
            yield return new WaitForSeconds(enemyScriptableObject.attackCooldown);
            if (!searchDetector.IsPlayerInRange) yield break;
            
            Vector2 direction = searchDetector.Player.transform.position - transform.position;
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
            _stateMachine.ChangeState(new AttackState(_agent, searchDetector.Player.transform));
        }
    }
}
