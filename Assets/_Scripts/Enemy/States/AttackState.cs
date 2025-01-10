using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Enemy.States
{
    public class AttackState : IState
    {
        public void Enter()
        {
            Debug.Log("Attack");
        }

        public void Stay()
        {
            
        }

        public void Exit()
        {
            
        }

        public void OnDestroy()
        {
            
        }
    }
}