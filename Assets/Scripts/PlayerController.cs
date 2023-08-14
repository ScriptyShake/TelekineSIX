using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] float walkingSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float gravity;
    [SerializeField] private bool inAir = true;
    private float _direction;
    private Vector2 _velocity;
    [SerializeField] private ContactFilter2D filter2D;
    
    private Collider2D[] _resultsDown = new Collider2D[1];
    private Collider2D[] _resultsUp = new Collider2D[1];
    private Collider2D[] _resultsLeft = new Collider2D[1];
    private Collider2D[] _resultsRight = new Collider2D[1];
    
    [SerializeField] Transform boxColliderDown;
    [SerializeField] Transform boxColliderUp;
    [SerializeField] Transform boxColliderLeft;
    [SerializeField] Transform boxColliderRight;

    #endregion
    
    
    void Start()
    {

    }
    
    void Update()
    {
        // Horizontal movement
        _direction = Input.GetAxis("Horizontal");
        _velocity.x = walkingSpeed * _direction;
        
        // Apply gravity
        _velocity.y -= gravity * Time.deltaTime;

        #region Collision

        // Ground/Down Collision Detection
        if (Physics2D.OverlapBox(boxColliderDown.position, boxColliderDown.localScale, 0, filter2D, _resultsDown) > 0 && _velocity.y < 0)
        {
            _velocity.y = 0;
            inAir = false;
        }
        else
        {
            inAir = true;
        }
        
        // Up Collision Detection
        if (Physics2D.OverlapBox(boxColliderUp.position, boxColliderUp.localScale, 0, filter2D, _resultsUp) > 0 && _velocity.y > 0)
        {
            _velocity.y = 0;
        }
        
        // Left Collision Detection
        if (Physics2D.OverlapBox(boxColliderLeft.position, boxColliderLeft.localScale, 90, filter2D, _resultsLeft) > 0 && _velocity.x < 0)
        {
            _velocity.x = 0;
        }
        
        // Right Collision Detection
        if (Physics2D.OverlapBox(boxColliderRight.position, boxColliderRight.localScale, 90, filter2D, _resultsRight) > 0 && _velocity.x > 0)
        {
            _velocity.x = 0;
        }

        #endregion
        

        // Jump
        if (Input.GetButtonDown("Jump") && !inAir)
        {
            _velocity.y = jumpSpeed;
        }
        
        // Final movement
        transform.Translate(_velocity * Time.deltaTime);
    }
}
