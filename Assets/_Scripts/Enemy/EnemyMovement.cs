using _Scripts.Enemy.States;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;

        private StateMachine _stateMachine;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            
            _stateMachine = new StateMachine(new PatrolState(this, _agent, patrolPoints));
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }
    }
}
