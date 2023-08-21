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
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _gravityTemp;
    [SerializeField] private bool _inAir;
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

        /*// Increase gravity when falling
        if (_rigidbody2D.velocity.y < 0 && _inAir)
        {
            Debug.Log("GravityTemp: " + _gravityTemp);
            Debug.Log("velocity.y " + _velocity.y);
            //_gravityTemp *= 1 + (gravityMultiplier * Time.deltaTime);
            // version of this that works with negative gravity multiplier

            _gravityTemp = 200;

            _velocity.y = Mathf.Max(_rigidbody2D.velocity.y, -maxFallSpeed);
        }
        else if (!_inAir)
        {
            _gravityTemp = gravity;
        }*/

        // Jump
        if (Input.GetButtonDown("Jump") && !_inAir)
        {
            _velocity.y = jumpSpeed;
        }

        // Final movement
        _rigidbody2D.velocity = _velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _inAir = false;
            Debug.Log("OnCollisionEnter2D: Landed on platform");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            _inAir = true;
            Debug.Log("OOnCollisionExit2D: Left the platform");
        }
    }
}