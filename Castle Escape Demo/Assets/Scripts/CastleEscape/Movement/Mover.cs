using System;
using CastleEscape.Attributes;
using CastleEscape.Core;
using UnityEngine;
using UnityEngine.AI;

namespace CastleEscape.Movement
{
    public class Mover : MonoBehaviour,IAction
    {
        private NavMeshAgent _playerNavMesh;
        private Animator _characterAnimator;
        private Health _health;

        private void Awake()
        {
            _playerNavMesh = GetComponent<NavMeshAgent>();
            _characterAnimator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            
        }
        
        void Update()
        {
            _playerNavMesh.enabled = !_health.IsDead(); // will be edited after death check
            
            //UpdateAnimator();
        }
        
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            //GetComponent<Fighter>().Cancel(); (inefficient line after dependency inversion)
            MoveTo(destination);
        }
        
        public void MoveTo(Vector3 destination)
        {
            _playerNavMesh.destination = destination;
            _playerNavMesh.isStopped = false;
        }
        
        public void Cancel()
        {
            _playerNavMesh.isStopped = true;
        }
        
        // Animation Velocity Equalized to NavMesh Velocity
        // InverseTransformDirection turns Worldspace (Global) Velocity to Local Velocity relative to NavMesh Agent.
        
        /*
        private void UpdateAnimator()
        {
            Vector3 characterVelocity = _playerNavMesh.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(characterVelocity);
            float speed = localVelocity.z;

            _characterAnimator.SetFloat("ForwardSpeed", speed);
            

        }
        
        */
        
    }
}
