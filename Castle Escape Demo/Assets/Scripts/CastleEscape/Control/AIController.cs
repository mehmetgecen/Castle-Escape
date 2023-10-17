using System;
using CastleEscape.Combat;
using CastleEscape.Movement;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

namespace CastleEscape.Control
{
    public enum AlertStage
    {
        Peaceful,
        Suspicious,
        Alerted
    }
    
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float waypointDwellTime = 2f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float patrolMovementSpeed = 2f;
        [SerializeField] private float attackMovementSpeed = 5f;
        [SerializeField] PatrolPath patrolPath;
        
        public AlertStage alertStage;
        public float fieldOfView;
        [Range(0,360)] public float fieldOfViewAngle;
        [Range(0,100)] public float alertLevel;
        
        
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

            alertStage = AlertStage.Peaceful;
            alertLevel = 0;

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
            alertStage = AlertStage.Alerted;
            
            GetComponent<NavMeshAgent>().speed = attackMovementSpeed;
            _fighter.Attack(_player);
            
            print("Attack Behaviour Called from AIController.");
            
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
        
        /*
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDistance);

            if (_player != null)
            {
                Gizmos.DrawLine(transform.position,_player.transform.position);  
            }
            
        }
        
        */
        
    }
}
