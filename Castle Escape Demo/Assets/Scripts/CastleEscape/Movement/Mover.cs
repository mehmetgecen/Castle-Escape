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
        private Health _health;

        private void Awake()
        {
            _playerNavMesh = GetComponent<NavMeshAgent>();
            _health = GetComponent<Health>();
            
        }
        
        void Update()
        {
            _playerNavMesh.enabled = !_health.IsDead(); // will be edited after death check
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
        
        
    }
}
