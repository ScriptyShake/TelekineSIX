using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkingSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] bool inAir = true;
    [SerializeField] Transform boxCollider;
    [SerializeField] private ContactFilter2D filter2D;
    [SerializeField] float gravity;
    [SerializeField] float platformHeight;
    
    private Collider2D[] _results = new Collider2D[1];
    private float _direction;
    private Vector2 _velocity;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _direction = Input.GetAxis("Horizontal");

        //rigid_body.velocity = new Vector2(walking_speed * direction, rigid_body.velocity.y);
        _velocity.x = walkingSpeed * _direction;
        // Apply gravity
        _velocity.y -= gravity * Time.deltaTime;

        // Ground Collision Detection
        if (Physics2D.OverlapBox(boxCollider.position, boxCollider.localScale, 0, filter2D, _results) > 0 && _velocity.y < 0)
        {
            _velocity.y = 0;
            Vector2 surface = Physics2D.ClosestPoint(transform.position, _results[0]) + Vector2.up * platformHeight;
            transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
            inAir = false;
        }
        else
        {
            inAir = true;
        }

        // Jump
        if (Input.GetButtonDown("Jump") && !inAir)
        {
            _velocity.y = jumpSpeed;
        }
        
        // Final movement
        transform.Translate(_velocity * Time.deltaTime);
    }
}
