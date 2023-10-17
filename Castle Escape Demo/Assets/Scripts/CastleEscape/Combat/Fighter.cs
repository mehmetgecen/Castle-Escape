using CastleEscape.Attributes;
using CastleEscape.Core;
using CastleEscape.Movement;
using UnityEngine;

namespace CastleEscape.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private float attackDistance;
        
        private Health _target; // will be edited
       
        private float _timeSinceLastAttack = Mathf.Infinity;
    
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target == null) return;
            if (_target.IsDead()) return;
            
            if (_target!=null)
            {
                if (!IsInRange())
                {
                    GetComponent<Mover>().MoveTo(_target.transform.position); 
                    
                    print("moving towards player");
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                    
                    print("attacking player");
                }
            }
        }
    
        public Health GetTarget()
        {
            return _target;
        }
    
        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            
            if (_timeSinceLastAttack >= attackCooldown)
            {
                _timeSinceLastAttack = 0;
                TriggerAttackAnimations();
                print("Attack Behaviour Called.");
            }
        }

        private void TriggerAttackAnimations()
        {
            GetComponent<Animator>().ResetTrigger("CancelAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }
        
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health combatTargetHealth = combatTarget.GetComponent<Health>();
            
            return combatTargetHealth != null && !combatTargetHealth.IsDead();
        }
        
        public void Attack(GameObject combatTarget)
        {
            _target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }
        
        // Animation Event called by Unity
        private void Hit()
        {
            //find the target's health component
            
            if (_target == null) return;
            {
                // TODO edited after take damage method is edited 
                
                _target.TakeDamage(gameObject);
                
            }
            
        }
        
        private bool IsInRange()
        {
            return Vector3.Distance(_target.transform.position, transform.position) < attackDistance;
        }
        
        
        public void Cancel()
        {
            StopAttackAnimatons();
            _target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttackAnimatons()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("CancelAttack");
        }
    }
}
