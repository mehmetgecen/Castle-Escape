using System;
using CastleEscape.Combat;
using CastleEscape.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace CastleEscape.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float waypointDwellTime = 2f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float patrolMovementSpeed = 2f;
        [SerializeField] private float attackMovementSpeed = 5f;
        [SerializeField] PatrolPath patrolPath;
    
        private GameObject _player;
        private Mover _mover;
        private Fighter _fighter;
       
        private bool isDead = false;
        
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;
        private Vector3 _guardPos;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (isDead) return;

            if (IsAggrevated() && _fighter.CanAttack(_player))
            {
                AttackBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            
            UpdateTimers();
            
        }

        private void UpdateTimers()
        {
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            GetComponent<NavMeshAgent>().speed = patrolMovementSpeed;
            
            Vector3 nextPosition = _guardPos;
            
            if (patrolPath!= null)
            {
                if (AtWaypoint())
                { 
                    CycleWaypoint();
                    _timeSinceArrivedAtWaypoint = 0;
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint >=waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition);
            }
            
            
        }

        private void AttackBehaviour()
        {
            GetComponent<NavMeshAgent>().speed = attackMovementSpeed;
            _fighter.Attack(_player);
            
        }
        
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float joinPathDistance = Vector3.Distance(transform.position,GetCurrentWaypoint());
            return joinPathDistance < waypointTolerance;
        }
        
        private bool IsAggrevated()
        {
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.transform.position);
            return distanceToPlayer < chaseDistance;
        }
        
        // Called from Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);

            if (_player != null)
            {
                Gizmos.DrawLine(transform.position,_player.transform.position);  
            }
            
        }
        
    }
}
