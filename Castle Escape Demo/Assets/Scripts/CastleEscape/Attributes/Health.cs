using CastleEscape.Core;
using UnityEngine;
using UnityEngine.Events;

namespace CastleEscape.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDie;
    
        private GameObject _instigator = null;
        private bool _isDead = false;
        private int _level;
        
        //TODO - will be edited later, die/kill condition will be calculated based on the level of the attacker
        public void TakeDamage(GameObject instigator)
        {
            if (GetComponent<Health>()._level >= instigator.GetComponent<Health>()._level) return;
            
            onDie.Invoke();
            Die();
            
            //TODO Game Cycle after death
            // if player is dead, game restarts
            
            if (gameObject.CompareTag("Player"))
            {
                //GameOver(); 
            }

        }
    
        public bool IsDead()
        {
            return _isDead;
        }
    
        private void Die()
        {
            if (_isDead) return;
            
            _isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
        }
    }
}