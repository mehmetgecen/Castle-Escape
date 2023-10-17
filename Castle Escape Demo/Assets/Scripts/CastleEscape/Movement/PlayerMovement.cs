using System;
using CastleEscape.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace CastleEscape.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private FixedJoystick fixedJoystick;
        [SerializeField] Transform childTransform;
        [SerializeField] private float movementSpeed = 5f;
    
        private Vector3 _movement;
        
        private Health _playerHealth;
        private NavMeshAgent _playerNavMesh;
        private Animator _characterAnimator;

        private void Awake()
        {
            _playerNavMesh = GetComponent<NavMeshAgent>();
            _characterAnimator = GetComponent<Animator>();
            _playerHealth = GetComponent<Health>();
            
        }
        
        private void Start()
        {
            _characterAnimator.SetFloat("ForwardSpeed", 0);
        }

        private void Update()
        {
            GetJoystickInputValues();
            
        }
    
        private void FixedUpdate()
        {
            if (_playerHealth.IsDead())
            {
                return;
            }
            
            SetMovement();
            SetRotation();
            UpdateAnimator();
            
            print(GetVelocityVector());
        }
    
        private void GetJoystickInputValues()
        {
            _movement.x = fixedJoystick.Horizontal;
            _movement.z = fixedJoystick.Vertical;
        }
    
        private void SetMovement()
        {
            playerRigidbody.velocity = GetVelocityVector();
        }

        private void SetRotation()
        {
            if (_movement.x == 0 || _movement.z == 0) return;
        
            childTransform.rotation = Quaternion.LookRotation(GetVelocityVector());
        }

        private Vector3 GetVelocityVector()
        {
            return new Vector3(_movement.x,playerRigidbody.velocity.y,_movement.z)* movementSpeed * Time.fixedDeltaTime;
        }
        
        // Set Joystick Values to Animator Parameters
        private void UpdateAnimator()
        {
            Vector3 characterVelocity = GetVelocityVector();
            Vector3 localVelocity = transform.InverseTransformDirection(characterVelocity);
            float speed = Math.Abs(localVelocity.z) + Math.Abs(localVelocity.x);

            _characterAnimator.SetFloat("ForwardSpeed", speed);
            
            print("Character Velocity: " + characterVelocity);
            print("Speed: " + speed);

        }
        
    
    }
}
