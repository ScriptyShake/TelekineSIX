using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float gravity;
    [Space]
    [SerializeField] private Transform boxColliderDown;
    [SerializeField] private Transform boxColliderUp;
    [SerializeField] private Transform boxColliderLeft;
    [SerializeField] private Transform boxColliderRight;
    
    private bool _inAir = true;
    private ContactFilter2D _filter2D;
    private float _direction;
    private Vector2 _velocity;
    private Collider2D[] _resultsDown = new Collider2D[1];
    private Collider2D[] _resultsUp = new Collider2D[1];
    private Collider2D[] _resultsLeft = new Collider2D[1];
    private Collider2D[] _resultsRight = new Collider2D[1];
    
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
        if (Physics2D.OverlapBox(boxColliderDown.position, boxColliderDown.localScale, 0, _filter2D, _resultsDown) > 0 && _velocity.y < 0)
        {
            _velocity.y = 0;
            _inAir = false;
        }
        else
        {
            _inAir = true;
        }
        
        // Up Collision Detection
        if (Physics2D.OverlapBox(boxColliderUp.position, boxColliderUp.localScale, 0, _filter2D, _resultsUp) > 0 && _velocity.y > 0)
        {
            _velocity.y = 0;
        }
        
        // Left Collision Detection
        if (Physics2D.OverlapBox(boxColliderLeft.position, boxColliderLeft.localScale, 90, _filter2D, _resultsLeft) > 0 && _velocity.x < 0)
        {
            _velocity.x = 0;
        }
        
        // Right Collision Detection
        if (Physics2D.OverlapBox(boxColliderRight.position, boxColliderRight.localScale, 90, _filter2D, _resultsRight) > 0 && _velocity.x > 0)
        {
            _velocity.x = 0;
        }

        #endregion
        

        // Jump
        if (Input.GetButtonDown("Jump") && !_inAir)
        {
            _velocity.y = jumpSpeed;
        }
        
        // Final movement
        transform.Translate(_velocity * Time.deltaTime);
    }
}
