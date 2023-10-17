using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private FixedJoystick fixedJoystick;
    [SerializeField] Transform childTransform;
    [SerializeField] private float movementSpeed = 5f;
    
    private Vector3 _movement;
    
    private void Update()
    {
        GetJoystickInputValues();
    }
    
    private void FixedUpdate()
    {
        SetMovement();
        SetRotation();
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
    
}
