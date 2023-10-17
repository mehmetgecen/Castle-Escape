using CastleEscape.Core;
using CastleEscape.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace CastleEscape.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDie;
    
        private GameObject _instigator = null;
        private bool _isDead = false;
        
        
        //TODO - will be edited later, die/kill condition will be calculated based on the level of the attacker
        public void TakeDamage(GameObject instigator)
        {
            if (GetComponent<Experience>().GetLevel() >= instigator.GetComponent<Experience>().GetLevel())
            {
                instigator.GetComponent<Health>().Die();
                
                if (instigator.CompareTag("Player"))
                {
                    //GameOver(); 
                }
            }

            else
            {
                onDie.Invoke();
                Die();
                
                if (gameObject.CompareTag("Player"))
                {
                    //GameOver(); 
                }
            }
            
            //TODO Game Cycle after death
            // if player is dead, game restarts
            
            

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
            GetComponent<CapsuleCollider>().enabled = false;

        }
    }
}
