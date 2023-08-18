using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _gravityTemp;
    private bool _inAir;
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
        _velocity.y -= gravity * Time.deltaTime;

        // Increase gravity when falling
        if (_rigidbody2D.velocity.y < 0 && _inAir)
        {
            _gravityTemp *= gravityMultiplier;
            _velocity.y = Mathf.Max(_rigidbody2D.velocity.y, -maxFallSpeed);
        }
        else if (!_inAir)
        {
            _gravityTemp = gravity;
        }
        
        // Jump
        if (Input.GetButtonDown("Jump") && !_inAir)
        {
            _velocity.y = jumpSpeed;
        }
        
        // Final movement
        _rigidbody2D.velocity = _velocity;
    }
}
