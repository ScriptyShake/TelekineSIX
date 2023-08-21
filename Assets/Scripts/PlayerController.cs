using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravity;
   
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PlayerGroundCheck _groundCheckGameObject;
    
    private float _gravityTemp;
    private float _direction;
    private Vector2 _velocity;

    #endregion


    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gravityTemp = gravity;
    }

    void Update()
    {
        _velocity = _rigidbody2D.velocity;
        
        // Horizontal movement
        _direction = Input.GetAxis("Horizontal");
        _velocity.x = walkingSpeed * _direction;

        // Apply gravity
        _velocity.y -= _gravityTemp * Time.deltaTime;

        // Jump
        if (Input.GetButtonDown("Jump") && _groundCheckGameObject.maxJumpsTemp > 0)
        {
            _velocity.y = jumpSpeed;
            _groundCheckGameObject.maxJumpsTemp--;
        }

        // Final movement
        _rigidbody2D.velocity = _velocity;
    }
}