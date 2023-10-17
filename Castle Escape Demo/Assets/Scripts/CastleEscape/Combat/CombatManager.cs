using System;
using System.Collections.Generic;
using CastleEscape.Control;
using CastleEscape.Stats;
using UnityEngine;

namespace CastleEscape.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private float combatDistance = 10f;
       
        private Experience _playerExperience;
        private int _playerLevel;
        private float _detectionRadius = 5f;
        
        GameObject _nearestEnemy;
        Fighter _fighter;
        

        private void Awake()
        {
            _fighter = gameObject.GetComponent<Fighter>();
            _playerExperience = gameObject.GetComponent<Experience>();
        }

        private void Start()
        {
            _playerLevel = _playerExperience.GetLevel();
        }
        
        private bool IsInFightZone()
        {
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, _nearestEnemy.transform.position);
            return distanceToPlayer < combatDistance;
        }

        private void Update()
        {
            if (_nearestEnemy == null)
            {
                var enemies = FindObjectsOfType<AIController>();
                _nearestEnemy = GetNearestEnemy(enemies);
            }
            else
            {
                if (!IsInFightZone()) return;
                _fighter.Attack(_nearestEnemy);
                print("In fight zone");
            }
        }

        private GameObject GetNearestEnemy(AIController[] enemies)
        {
            GameObject nearestEnemy = null;
            float closestDistance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy.gameObject;
                }
            }

            return nearestEnemy;
        }
    }
}
